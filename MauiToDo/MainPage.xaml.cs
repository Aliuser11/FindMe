using MauiTodo.Data;
using MauiTodo.Models;
using System.Collections.ObjectModel;

namespace MauiToDo
{
    public partial class MainPage : ContentPage
    {
       
        
        readonly Database _database;

        public MainPage()
        {
            InitializeComponent();
            _database = new Database();
            _ = Initialize();

            TodosCollection.ItemsSource = Todos;
        }
        public ObservableCollection<TodoItem> Todos { get; set; } = new();

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            await App.Current.MainPage.
                DisplayAlert(item.Text, $"You invoked the {item.Text} action.", "OK");
        }

        private async Task Initialize()
        {
            var todos = await _database.GetTodos();
            foreach (var todo in todos)
            {
                Todos.Add(todo);
            }        
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var todo = new TodoItem
            {
                Due = DueDatepicker.Date,
                Title = TodoTitleEntry.Text
            };

            var inserted = await _database.AddTodo(todo);
            if (inserted != 0)
            {
                Todos.Add(todo);
                TodoTitleEntry.Text = string.Empty;
                DueDatepicker.Date = DateTime.Now;
            }
        }
    }

}
