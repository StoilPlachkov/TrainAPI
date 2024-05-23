using ApplicationServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.implementations
{
    public interface ITrainManagmentService
    {
        Task<List<TrainDTO>> GetAllTrains();
        Task<TrainDTO> GetTrain(int id);
        Task PostTrain(TrainDTO trainDTO);
        Task PutTrain(int id,TrainDTO trainDTO);
        Task DeleteTrain(int id);
    }
}
