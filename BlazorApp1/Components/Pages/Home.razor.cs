using BlazorApp1.Entities;
using Microsoft.AspNetCore.Http;

namespace BlazorApp1.Components.Pages
{
    public partial class Home
    {
        private List<TaskInfo> list = default!;
        bool sessionExsists => sessionState.HasState;

        protected override void OnInitialized()
        {

            list ??= new List<TaskInfo>
            {
                new TaskInfo("題名１","2024/10/28","0.未着手","内容１\n改行後１",""),
                new TaskInfo("題名２","2024/10/29","0.未着手","内容２\n改行後２",""),
                new TaskInfo("題名３","2024/10/30","1.仕掛中","内容３\n改行後３","")
            };

            foreach(var task in list)
            {
                DateTime date;
                bool success = DateTime.TryParse(task.Term, out date);

                if (success)
                {
                    TimeSpan span = date - DateTime.Today;
                    task.span = Math.Abs(span.Days).ToString();
                }
                else
                {
                    task.span = "";
                }
            }

            list.Sort((x, y) => y.Term.CompareTo(x.Term)); // 期限で降順に並び替え
            list.Sort((x, y) => x.span.CompareTo(y.span)); // spanで昇順に並び替え
            list.Sort((x, y) => x.State.CompareTo(y.State)); // 状態で昇順に並び替え

            if (sessionExsists)
            {
                list = sessionState.State!;
            }
        }

        async Task LoadTaskInfo()
        {
            await Task.Yield();

            if (sessionExsists)
            {
                sessionState.State = list;
                return;
            }
        }

        private void NavigateToTaskAddPage()
        {
            sessionState.State = list;

            NavManager.NavigateTo("taskadd");
        }

    }
}
