using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S4U.Application.Services.Interface
{
    public interface IHangfire
    {
        Task GetRealTimeData();
        Task SendPushNotes();
    }
}