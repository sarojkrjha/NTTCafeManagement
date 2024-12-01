using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetCafesQuery : IRequest<IEnumerable<CafeDto>>
    {
        public string? Location { get; set; }

        public GetCafesQuery(string? location)
        {
            Location = location;
        }
    }
}
