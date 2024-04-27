using System.Collections.ObjectModel;
using System.Net.Http.Json;
namespace MauiMovies;

public partial class MainPage : ContentPage
{
    string _apiKey = "[your API key]";
    string _baseUri = "https:/ /api.themoviedb.org/3/";

    private TrendingMovies _movieList;
    private GenreList _genres;
    public ObservableCollection<Genre> Genres { get; set; } = new();
    public ObservableCollection<MovieResult> Movies { get; set; } = new();
    public bool IsLoading { get; set; }
    private readonly HttpClient _httpClient;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
        _httpClient = new HttpClient { BaseAddress = new Uri(_baseUri) };
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        IsLoading = true;
        OnPropertyChanged(nameof(IsLoading));
        _genres = await _httpClient.GetFromJsonAsync<GenreList>($"genre/movie/list?api_key={_apiKey}&language=en-US");
        _movieList = await _httpClient.GetFromJsonAsync<TrendingMovies>($"trending/movie/week?api_key={_apiKey}&language=en-US");
        IsLoading = false;
        OnPropertyChanged(nameof(IsLoading));
    }
}