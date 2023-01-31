define([
    "dojo/_base/declare",
    "dijit/_WidgetBase",
    "dijit/_TemplatedMixin"
], function (
    declare,
    _WidgetBase,
    _TemplatedMixin

) {
    return declare("nitecoui/components/CustomPanelWidget",
        [_WidgetBase, _TemplatedMixin], {
            templateString: dojo.cache("/en/customview/")
    });
});