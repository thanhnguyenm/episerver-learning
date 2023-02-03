define([
    "dojo/_base/declare",
    "dijit/_WidgetBase",
    "dijit/_TemplatedMixin"
], function (
    declare,
    _WidgetBase,
    _TemplatedMixin

) {
    return declare("nitecoui/components/Test1Widget",
        [_WidgetBase, _TemplatedMixin], {
            templateString: "<div>hello world</div>"
    });
});