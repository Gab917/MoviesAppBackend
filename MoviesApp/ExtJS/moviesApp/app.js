/*
 * This file launches the application by asking Ext JS to create
 * and launch() the Application class.
 */
Ext.application({
    extend: 'moviesRentalApp.Application',

    name: 'moviesRentalApp',

    requires: [
        // This will automatically load all classes in the moviesRentalApp namespace
        // so that application classes do not need to require each other.
        'moviesRentalApp.*'
    ],

    // The name of the initial view to create.
    mainView: 'moviesRentalApp.view.main.Main'
});
