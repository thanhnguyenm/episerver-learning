define([
    "dojo/_base/declare",
    "nitecoui/components/UserMessageViewModel",
    "epi-cms/asset/HierarchicalList",
    "dijit/_WidgetBase",
    "epi/i18n!epi/cms/nls/episerver.cms.components.media" // Location of EPiServers translation file with dot-notation for XML structure.
],
    // Parameters must match import statements above.
    function (declare, UserMessageViewModel, HierarchicalList, _WidgetBase, resources) {
        return declare("nitecoui/components/UserMessageComponent",
            [HierarchicalList],
            {
                noDataMessage: resources.nocontent,
                noDataMessages: resources.nodatamessages,
            });
    });