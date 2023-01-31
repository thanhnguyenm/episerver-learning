define([
    "dojo/_base/declare",
    "dijit/_Widget",
    "dijit/_TemplatedMixin",
    'dojo/text!nitecoui/Editors/Templates/ExampleString.html',
],
    function (
        declare,
        _Widget,
        _TemplatedMixin,
        template
    ) {
        return declare("nitecoui/editors/ExampleString", [_Widget, _TemplatedMixin], {
            templateString: template
        });
    });