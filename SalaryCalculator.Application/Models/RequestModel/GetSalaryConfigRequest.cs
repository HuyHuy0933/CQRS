using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core.Framework;

namespace SalaryCalculator.Application.Models.RequestModel
{
    public class GetSalaryConfigRequest : IRequest<Result<GetSalaryConfigResponse>>
    {

    }
}
