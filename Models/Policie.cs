using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Policies.Models
{
    [BsonIgnoreExtraElements]
    public class Policie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("numero_poliza")]
        public int NumeroPoliza { get; set; }

        [BsonElement("nombre_cliente")]
        public string NombreCliente { get; set; } = String.Empty;

        [BsonElement("identificacion_cliente")]
        public string IdentificacionCliente { get; set; } = String.Empty;

        [BsonElement("fecha_nacimiento_cliente")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime FechaNacimientoCliente { get; set; }

        [BsonElement("fecha_registro_poliza")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime FechaRegistroPoliza { get; set; }

        [BsonElement("cobertura_polizas")]
        public List<string> CoberturasPolizas { get; set; }

        [BsonElement("valor_max_poliza")]
        public double ValorMaximoPoliza { get; set; }

        [BsonElement("nombre_plan_poliza")]
        public string NombrePlanPoliza { get; set; } = String.Empty;

        [BsonElement("ciudad_residenca_cliente")]
        public string CiudadResidenciaCliente { get; set; } = String.Empty;

        [BsonElement("direccion_residencia_cliente")]
        public string DireccionResidenciaCliente { get; set; } = String.Empty;

        [BsonElement("placa_automotor")]
        public string PlacaAutoMotor { get; set; } = String.Empty;

        [BsonElement("modelo_automotor")]
        public string ModeloAutoMotor { get; set; } = String.Empty;

        [BsonElement("vehiculo_inspeccion")]
        public bool VehiculoInspeccion { get; set; }

        [BsonElement("fecha_inicio_vigencia")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime FechaInicioVigencia { get; set; }

        [BsonElement("fecha_fin_vigencia")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime FechaFinVigencia { get; set; }
    }
}