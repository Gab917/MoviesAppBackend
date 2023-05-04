/**
 * This class is the main view for the application. It is specified in app.js as the
 * "mainView" property. That setting automatically applies the "viewport"
 * plugin causing this view to become the body element (i.e., the viewport).
 *
 * TODO - Replace this content of this view to suite the needs of your application.
 */
Ext.define('moviesRentalApp.view.main.Main', {
    extend: 'Ext.tab.Panel',
    xtype: 'app-main',

    requires: [
        'Ext.plugin.Viewport',
        'Ext.window.MessageBox',

        'moviesRentalApp.view.main.MainController',
        'moviesRentalApp.view.main.MainModel',
        'moviesRentalApp.view.main.List',

        'moviesRentalApp.view.MovieList',
        'moviesRentalApp.viewmodel.MovieListViewModel'
    ],

    controller: 'main',
    viewModel: {
        type: 'main'
    },
    viewModel: {
        type: 'movielist'
    },

    ui: 'navigation',

    tabBarHeaderPosition: 1,
    titleRotation: 0,
    tabRotation: 0,

    header: {
        layout: {
            align: 'stretchmax'
        },
        title: {
            bind: {
                text: 'Movie Rental App'
            },
            flex: 0
        },
        iconCls: 'fa-th-list'
    },

    tabBar: {
        flex: 1,
        layout: {
            align: 'stretch',
            overflowHandler: 'none'
        }
    },

    responsiveConfig: {
        tall: {
            headerPosition: 'top'
        },
        wide: {
            headerPosition: 'left'
        }
    },

    defaults: {
        bodyPadding: 30,
        tabConfig: {
            responsiveConfig: {
                wide: {
                    iconAlign: 'left',
                    textAlign: 'left'
                },
                tall: {
                    iconAlign: 'top',
                    textAlign: 'center',
                    width: 120
                }
            }
        }
    },


    items: [{
        title: 'Movies',
        iconCls: 'fa-film',
        // The following grid shares a store with the classic version's grid as well!
        items: [{
            //xtype: 'mainlist'
            xtype: 'moviegrid'
        }]
    }, {
        title: 'Customers',
        iconCls: 'fa-users',
        bind: {
            html: '{loremIpsum}'
        },
        items: [{
            xtype: 'mainlist'
        }]
    }, {
        title: 'Rent a movie',
        iconCls: 'fa-compact-disc',
        bind: {
            html: '{loremIpsum}'
        }
    }]
});
