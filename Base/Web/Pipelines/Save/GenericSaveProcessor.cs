using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.Save;
using SitecoreSuperchargers.GenericItemProvider.Data;

namespace SitecoreSuperchargers.GenericItemProvider.Pipelines.Save
{
    public class GenericSaveProcessor
    {
        //public int Limit { get; set; }

        //public void Process(SaveArgs args)
        //{
        //    Assert.ArgumentNotNull(args, "args");

        //    if (!args.HasSheerUI) return;

        //    if (args.IsPostBack)
        //    {
        //        if (args.Result == "no" || args.Result == "undefined")
        //        {
        //            args.AbortPipeline();
        //        }

        //        return;
        //    }

        //    Assert.IsNotNull(Context.ContentDatabase, "Sitecore.Context.ContentDatbabase");

        //    // For each of the items the user attempts to save
        //    foreach (var uiItem in args.Items)
        //    {
        //        // Retrieve from the database the same item containing the old values
        //        var dbItem = Context.ContentDatabase.GetItem(uiItem.ID, uiItem.Language, uiItem.Version);
        //        Assert.IsNotNull(dbItem, "dbItem");

        //        if (!dbItem.IsEntity()) continue;

        //        var changedFields =
        //            (from uiField in uiItem.Fields
        //             where uiField.Value != dbItem[uiField.ID]
        //             select dbItem.Fields[uiField.ID].Name).ToList();

        //        if (changedFields.Any())
        //        {
        //            var savable = Helpers.EntityHelper.CreateSavableInstance(dbItem);
        //        }
        //    }

        //    args.SaveAnimation = false;
        //    args.AbortPipeline();
        //}
    }
}