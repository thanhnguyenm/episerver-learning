define([
    "dojo/_base/declare",
    "nitecoui/components/CommentViewModel",
    "epi-cms/asset/HierarchicalList",
    "dijit/_WidgetBase",
    "epi/i18n!epi/cms/nls/episerver.cms.components.media" // Location of EPiServers translation file with dot-notation for XML structure.
],
    // Parameters must match import statements above.
    function (declare, CommentViewModel, HierarchicalList, _WidgetBase, resources) {
        return declare("nitecoui/components/CommentComponent",
            [HierarchicalList],
            {
                //res: resources,
                //createContentText: "Click to create item",
                //enableDndFileDropZone: false,
                //showCreateContentArea: true,
                //showThumbnail: true,
                //modelClassName: CommentViewModel,
                noDataMessage: resources.nocontent,
                noDataMessages: resources.nodatamessages,
                //hierarchicalListClass: "epi-mediaList",

                //_onCreateAreaClick: function () {
                //    // summary:
                //    //      A callback function which is executed when the create area is clicked.
                //    // tags:
                //    //      protected
                //    this.inherited(arguments);
                //    this.model._commandRegistry.newComment.command.execute();
                //}
            });
    });