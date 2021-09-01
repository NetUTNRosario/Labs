using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unidad._5.Lab._1.MVC.Models
{
    public interface IPlanRepository
    {
        Plan? GetOnePlan(int planId);
        IEnumerable<Plan> GetPlanes();
    }

    public class PlanRepository : IPlanRepository
    {
        /// <summary>
        /// Defines the _planes.
        /// </summary>
        private List<Plan> _planes;
        public PlanRepository()
        {
            _planes = new List<Plan>
            {
                new()
                {
                    Id=1,
                    Anio = 2008,
                    Especialidad = "Ingeniería en Sistemas de Información"
                },
                new()
                {
                    Id=2,
                    Anio = 2008,
                    Especialidad  = "Ingeniería Quimica"
                },
            };
        }
        /// <summary>
        /// The GetPlanes.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Plan}"/>.</returns>
        public IEnumerable<Plan> GetPlanes() => _planes;

        /// <summary>
        /// The GetOnePlan.
        /// </summary>
        /// <param name="planId">The planId<see cref="int"/>.</param>
        /// <returns>The <see cref="Plan"/>.</returns>
        public Plan? GetOnePlan(int planId) => _planes.Find(p => p.Id == planId);
    }
}
