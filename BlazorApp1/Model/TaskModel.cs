using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Model
{
    public enum StateType
    {
        未着手 = 0,
        仕掛中 = 1,
        完了 = 2,
        無視 = 9
    };

    public class TaskModel
    {
        //[Required(ErrorMessage = "必須入力")]
        public string Title { get; set; }
        //[Required(ErrorMessage = "必須入力")]
        public DateTime Term { get; set; }
        //[Required(ErrorMessage = "必須入力")]
        public StateType State { get; set; }
        public string Details { get; set; }
    }

}
