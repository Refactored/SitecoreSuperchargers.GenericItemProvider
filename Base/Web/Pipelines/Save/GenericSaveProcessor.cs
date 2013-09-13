using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using Sitecore.Pipelines.Save;
using SitecoreSuperchargers.GenericItemProvider.Data;

namespace SitecoreSuperchargers.GenericItemProvider.Pipelines.Save
{
    public class GenericSaveProcessor
    {
        public int Limit { get; set; }

        public void Process(SaveArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            if (args.IsPostBack)
            {
                if (args.Result == "no" || args.Result == "undefined") args.AbortPipeline();
                return;
            }

            Assert.IsNotNull(Context.ContentDatabase, "Sitecore.Context.ContentDatbabase");

            foreach (var uiItem in args.Items)
            {
                // Retrieve from the database the same item containing the old values
                var item = Context.ContentDatabase.GetItem(uiItem.ID, uiItem.Language, uiItem.Version);
                if (null == item) continue;
                if (Helpers.ItemHelper.IsIgnorable(item)) continue;

                // Make sure this is an ISavable entity before continuing.
                var savable = Helpers.EntityHelper.CreateSavableInstance(item);
                if (savable == null) return;

                // Get a copy of the original item to pass to the Save method.
                var originalItem = item.Database.GetItem(item.ID, item.Language, item.Version);

                // --- Get field changes which is passed to the Save method.
                var changes = Helpers.ItemHelper.GetFieldChanges(item, originalItem);

                // Now call the ISavable.Save method which will trigger custom save logic.
                var saved = savable.Save(item, originalItem, changes);
                if (saved) continue;

                args.AddMessage("External save failed.", PipelineMessageType.Error);
                args.SaveAnimation = false;
                args.AbortPipeline();
            }
        }
    }
}