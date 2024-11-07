using BlazorApp1.Entities;
using BlazorApp1.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using System.Windows.Input;
using Microsoft.JSInterop;

namespace BlazorApp1.Components.Pages
{
    public partial class TaskAdd
    {

        private TaskModel taskmodel = new TaskModel
        {
            Title = string.Empty,
            Term = DateTime.Now,
            State = StateType.未着手,
            Details = string.Empty
        };

        private void Cancel()
        {
            NavManager.NavigateTo(NavManager.BaseUri);
        }

        private void AddTask()
        {

            if (TaskCheck())
            {
                return;
            }

            List<TaskInfo> list = default!;
            string state = string.Empty;
            state = taskmodel.State.ToString();

            switch (taskmodel.State)
            {
                case StateType.未着手:
                    state = "0." + state;
                    break;
                case StateType.仕掛中:
                    state = "1." + state;
                    break;
                case StateType.完了:
                    state = "2." + state;
                    break;
                case StateType.無視:
                    state = "9." + state;
                    break;
            }

            list = sessionState.State!;
            list.Add(new TaskInfo(taskmodel.Title, taskmodel.Term.ToString("yyyy/MM/dd"), state, taskmodel.Details, ""));

            foreach (var task in list)
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

            sessionState.State = list;

            NavManager.NavigateTo(NavManager.BaseUri);
        }
        private System.Boolean TaskCheck()
        {

            if (taskmodel.Title == string.Empty)
            {
                ShowMessageBox("題名エラー");
                return true;
            }

            DateTime newDate;
            if (DateTime.TryParse(taskmodel.Term.ToString(),out newDate))
            {
                //変換成功
            }
            else
            {
                ShowMessageBox("期限エラー");
                return true;
            }
            
            if ((taskmodel.State != StateType.未着手) && 
                (taskmodel.State != StateType.仕掛中) &&
                (taskmodel.State != StateType.完了) &&
                (taskmodel.State != StateType.無視) 
               )
            {
                ShowMessageBox("状態エラー");
                return true;
            }

            return false;

        }

        private void ShowMessageBox(string message)
        {
            JSRuntime.InvokeVoidAsync("alert", message);
        }

    }
}
