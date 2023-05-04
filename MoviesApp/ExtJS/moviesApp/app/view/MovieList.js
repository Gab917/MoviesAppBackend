Ext.define('moviesRentalApp.view.MovieList', {
    extend: 'Ext.grid.Panel',
    xtype: 'moviegrid',
    
    requires: [
        'moviesRentalApp.viewmodel.MovieListViewModel',
        'moviesRentalApp.store.Movies'
    ],

    viewModel: 'viewmodel.movielist',

    title: 'Movies',
    store: {
        type: 'movies'
    },

    columns: [
        { text: 'ID', dataIndex: 'movieId', width: 50 },
        { text: 'Title', dataIndex: 'title', flex: 1 },
        { text: 'Description', dataIndex: 'description', flex: 2 },
        { text: 'Genre', dataIndex: 'genre', flex: 1 },
        { text: 'Release Date', dataIndex: 'releaseDate', flex: 1 }
    ]
});