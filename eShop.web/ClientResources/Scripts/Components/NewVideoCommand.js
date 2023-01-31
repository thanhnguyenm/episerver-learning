define("nitecoui/components/NewVideoCommand",
    [
        "dojo/_base/declare",
        "dojo/topic",
        "epi/shell/TypeDescriptorManager",
        "epi-cms/command/NewContent"
    ],
    function (declare, topic, TypeDescriptorManager, NewContentCommand) {

        return declare([NewContentCommand],
            {
                _execute: function () {
                    // summary:
                    //      Executes this command; publishes a change view request to change to the create content view.
                    // tags:
                    //      protected
                    topic.publish("/epi/shell/action/changeview",
                        "epi-cms/contentediting/CreateContent",
                        null,
                        {
                            requestedType: this.contentType,
                            editAllPropertiesOnCreate: this.editAllPropertiesOnCreate,
                            parent: this.model,
                            autoPublish: this.autoPublish,
                            createAsLocalAsset: this.createAsLocalAsset,
                            view: TypeDescriptorManager.getValue(this.contentType, "createView"),
                            addToDestination: {
                                save: function () {
                                }
                            }
                        });
                }
            });
    });