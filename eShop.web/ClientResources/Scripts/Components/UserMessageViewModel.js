define("nitecoui/components/UserMessageViewModel",
    [
        "dojo/_base/declare",
        "dojo/_base/lang",
        "epi-cms/asset/view-model/HierarchicalListViewModel",
        "epi/i18n!epi/cms/nls/episerver.cms.components.media"
    ],
    function (
        declare,
        lang,
        HierarchicalListViewModel,
        resources
    ) {

        return declare([HierarchicalListViewModel],
            {
                // summary:
                //      Handles search and tree to list browsing widgets.
                // tags:
                //      internal
                _getTypesToCreate: function () {
                    return [];
                },
                _setupCommands: function () {
                    // summary:
                    //      Creates and registers the commands used.
                    // tags:
                    //      protected
                    //
                    var tempCreatableTypes = this.creatableTypes;
                    //assign creatableTypes to null temporarily to avoid the automatically generated New Item option in the context menu
                    this.creatableTypes = null;
                    this.inherited(arguments);
                    this.creatableTypes = tempCreatableTypes;
                    this.noDataMessages = resources.nodatamessages;
                    this.noDataMessage = resources.nocontent;
                    var customCommands = {
                        newComment: {
                            command: new NewCommentCommand(lang.mixin({
                                iconClass: "epi-iconPlus",
                                label: "New comment",
                                canExecute: true, //XYI: to enable the button
                                viewModel: this,
                                contentType: this.creatableTypes[0],
                                createAsLocalAsset: false,
                                autoPublish: true
                            }))
                        },
                        newCommentContext: {
                            command: new NewCommentCommand(lang.mixin({
                                category: "context",
                                iconClass: "epi-iconPlus",
                                label: "New comment",
                                viewModel: this,
                                contentType: this.creatableTypes[0],
                                createAsLocalAsset: false,
                                autoPublish: true
                            })),
                            isAvailable: this.treeStoreModel.root.name === "ROOT" | this.treeStoreModel.root.name === "TREE",
                            order: 2
                        }
                    };
                    this._commandRegistry = lang.mixin(this._commandRegistry, customCommands);
                    this.pseudoContextualCommands.push(this._commandRegistry.newComment.command);
                    this.pseudoContextualCommands.push(this._commandRegistry.newCommentContext.command);
                },
                _updateTreeContextCommandModels: function (model) {
                    // summary:
                    //      Update model of commands in case selected content is folder
                    // tags:
                    //      private
                    this.inherited(arguments);
                    this._commandRegistry.newComment.command.set("model", model);
                    this._commandRegistry.newCommentContext.command.set("model", model);
                },
                _selectedTreeItemsSetter: function (selectedItems) {
                    this._commandRegistry.newComment.command.set("model", selectedItems);

                    this.inherited(arguments);
                }
            });

    });