﻿using System;
using System.Threading.Tasks;
using MySample.Core.Models;

namespace MySample.Core.InfrastructureInterfaces
{
    public interface IMyModelWriteRepository
    {
        Task Create(MyModel model);
        Task Update(MyModel model);
        Task Delete(Guid id);
    }
}