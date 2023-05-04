Ext.define('moviesRentalApp.viewmodel.MovieListViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.movielist',
    
    stores: {
        movies: {
            type: 'movies'
        }
    }
    
    
});