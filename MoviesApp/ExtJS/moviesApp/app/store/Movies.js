Ext.define('moviesRentalApp.store.Movies', {
    extend: 'Ext.data.Store',
    alias: 'store.movies',

    requires: [
        'moviesRentalApp.model.Movie'
    ],

    model: 'moviesRentalApp.model.Movie',

    proxy: {
        type: 'rest',
        url: '/api/Movies',
        reader: {
            type: 'json'
        }
    },

    autoLoad: true
});