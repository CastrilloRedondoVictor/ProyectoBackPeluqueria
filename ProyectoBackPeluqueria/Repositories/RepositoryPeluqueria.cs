using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoBackPeluqueria.Data;
using ProyectoBackPeluqueria.Models;
using System.Data;

namespace ProyectoBackPeluqueria.Repositories
{
    public class RepositoryPeluqueria
    {
        private readonly AppDbContext _context;

        public RepositoryPeluqueria(AppDbContext context)
        {
            _context = context;
        }

        // Obtener servicios
        public async Task<List<Servicio>> ObtenerServiciosAsync()
        {
            return await _context.Servicios.ToListAsync();
        }

        // Obtener disponibilidad por fecha
        public async Task<List<HorarioDisponible>> ObtenerDisponibilidadAsync(DateTime fecha)
        {
            return await _context.HorariosDisponibles
                .FromSqlRaw("EXEC ObtenerDisponibilidad @Fecha", new SqlParameter("@Fecha", fecha))
                .ToListAsync();
        }

        // Obtener días disponibles
        public async Task<List<DateTime>> ObtenerDiasDisponiblesAsync()
        {
            var dias = await _context.HorariosDisponibles
                .FromSqlRaw("EXEC ObtenerDiasDisponibles")
                .Select(h => h.Fecha)
                .Distinct()
                .ToListAsync();

            return dias;
        }

        // Insertar reserva
        public async Task InsertarReservaAsync(int clienteId, int servicioId, DateTime fechaHoraInicio)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC InsertarReservaSimple @ClienteId, @ServicioId, @FechaHoraInicio",
                new SqlParameter("@ClienteId", clienteId),
                new SqlParameter("@ServicioId", servicioId),
                new SqlParameter("@FechaHoraInicio", fechaHoraInicio)
            );
        }

        // Realizar compra
        public async Task RealizarCompraAsync(int clienteId, DataTable detallesCompra)
        {
            var paramCliente = new SqlParameter("@ClienteId", clienteId);
            var paramDetalles = new SqlParameter("@DetallesCompra", detallesCompra)
            {
                SqlDbType = SqlDbType.Structured,
                TypeName = "Tipo_CompraDetalles"
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC RealizarCompra @ClienteId, @DetallesCompra",
                paramCliente, paramDetalles
            );
        }

        // Obtener horarios disponibles por servicio y fecha
        public async Task<List<HorarioDisponible>> ObtenerHorariosDisponiblesPorFechaAsync(int servicioId, DateTime fecha)
        {
            return await _context.HorariosDisponibles
                .FromSqlRaw("EXEC ObtenerHorariosDisponiblesPorFecha @ServicioId, @Fecha",
                    new SqlParameter("@ServicioId", servicioId),
                    new SqlParameter("@Fecha", fecha))
                .ToListAsync();
        }

        // Obtener todos los días y horas disponibles por servicio
        public async Task<List<HorarioDisponible>> ObtenerDiasYHorasDisponiblesAsync(int servicioId)
        {
            return await _context.HorariosDisponibles
                .FromSqlRaw("EXEC ObtenerDiasYHorasDisponibles @ServicioId",
                    new SqlParameter("@ServicioId", servicioId))
                .ToListAsync();
        }

        public async Task<List<Usuario>> GetClientesAsync()
        {
            return await _context.Usuarios
                .FromSqlRaw("SELECT * FROM Usuarios WHERE IdRolUsuario = 1")
                .ToListAsync();
        }

        public async Task<List<ReservaView>> ObtenerReservasClientesAsync()
        {
            return await _context.VistaReservas
                .FromSqlRaw("SELECT * FROM Vista_Reservas")
                .ToListAsync();
        }
    }
}
