define([
    "dojo/_base/declare",
    "dijit/_WidgetBase",
    "dijit/_TemplatedMixin"
], function (
    declare,
    _WidgetBase,
    _TemplatedMixin

) {
    return declare("nitecoui/components/UserManualWidget",
        [_WidgetBase, _TemplatedMixin], {
        templateString: dojo.cache("/Static/html/UserGuide/ContentEditorsUserManual.html")
    });
});