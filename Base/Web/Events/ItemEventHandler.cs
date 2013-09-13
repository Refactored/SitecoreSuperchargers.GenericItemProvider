using Sitecore;
using Sitecore.Data.Events;
using Sitecore.Data.Items;
using Sitecore.Events;
using SitecoreSuperchargers.GenericItemProvider.Data;
using System;
using System.Linq;

namespace SitecoreSuperchargers.GenericItemProvider.Events
{
    public class ItemEventHandler
    {
        //public void OnItemCreated(object sender, EventArgs args)
        //{
        //    var e = Event.ExtractParameter<ItemCreatedEventArgs>(args, 0);
        //    if (null == e) return;

        //    var item = e.Item;
        //    if (null == item) return;
        //    if (IsIgnorable(item)) return;

        //    // Make sure this is an ICreatable entity before continuing.
        //    var creatable = Helpers.EntityHelper.CreateCreatableInstance(item);
        //    if (creatable == null) return;

        //    // Now call the ICreatable.Create method which will trigger custom save logic.
        //    var saved = creatable.Create(item);
        //    if (saved) return;

        //    // TODO: Handle a failed external save (by cleaning up the created item).
        //}

        //public void OnItemSaving(object sender, EventArgs args)
        //{
        //    var item = Event.ExtractParameter<Item>(args, 0);
        //    if (null == item) return;
        //    if (Helpers.ItemHelper.IsIgnorable(item)) return;

        //    // Make sure this is an ISavable entity before continuing.
        //    var savable = Helpers.EntityHelper.CreateSavableInstance(item);
        //    if (savable == null) return;

        //    // Get a copy of the original item to pass to the Save method.
        //    var originalItem = item.Database.GetItem(item.ID, item.Language, item.Version);

        //    // --- Get field changes which is passed to the Save method.
        //    var changes = Helpers.ItemHelper.GetFieldChanges(item, originalItem);

        //    // Now call the ISavable.Save method which will trigger custom save logic.
        //    var saved = savable.Save(item, originalItem, changes);
        //    if (saved) return;

        //    // External data store save failed, abort save process.
        //    ((SitecoreEventArgs) args).Result.Cancel = true;

        //    // Force UI to refresh to previous item values.
        //    var load =
        //        String.Concat(new object[]
        //            {"item:load(id=", item.ID, ",language=", item.Language, ",version=", item.Version, ")"});
        //    Context.ClientPage.SendMessage(this, load);
        //}
    }
}