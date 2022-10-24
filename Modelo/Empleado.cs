using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace t1-back-front {
  public class Empleado {
    ConexionBD conectar;

    private DataTable drop_puesto() {
      DataTable tabla = new DataTable();
      conectar = new ConexionBD();
      conectar.AbrirConexion();

      string strConsulta = string.Format("SELECT id_puesto AS id, puesto FROM puesto;");
      MySqlDataAdapter consulta = new MySqlDataAdapter(strConsulta, conectar.conectar);
      consulta.Fill(tabla);
      conectar.CerarConexion();
      return tabla;
    }

    public void drop_puesto(DropDownList drop) {
      drop.ClearSelection();
      drop.Items.Clear();
      drop.AppendDataBoundItems = true;
      drop.Items.Add("<< Elige un Tipo de Puesto >>");
      drop.Items[0].Value = "0";
      drop.DataSource = drop_puesto();
      drop.DataTextField = "puesto";
      drop.DataValueField = "id";
      drop.DataBind();
    }

    private DataTable grid_empleados() {
      DataTable tabla = new DataTable();
      conectar = new ConexionBD();
      conectar.AbrirConexion();

      String consulta = string.Format("SELECT e.*, ts.puesto FROM empleados e INNER JOIN puesto ts ON ts.id_puesto=e.id_puesto;");
      MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
      query.Fill(tabla);
      conectar.CerarConexion();
      return tabla;
    }

    public void grid_empleados(GridView grid) {
      grid.DataSource = grid_empleados();
      grid.DataBind();
    }

    public int agregar(string codigo, string nombres, string apellidos, string direccion, string telefono, string fecha_nacimiento, int id_puesto) {
      int no_ingreso = 0;
      conectar = new ConexionBD();
      conectar.AbrirConexion();

      string strConsulta = string.Format("INSERT INTO empleados(codigo,nombres,apellidos,direccion,telefono,fecha_nacimiento,id_puesto) VALUES('{0}','{1}','{2}','{3}','{4}','{5}',{6});", codigo, nombres, apellidos, direccion, telefono, fecha_nacimiento, id_puesto);
      MySqlCommand insertar = new MySqlCommand(strConsulta, conectar.conectar);
      insertar.Connection = conectar.conectar;
      no_ingreso = insertar.ExecuteNonQuery();
      conectar.CerarConexion();
      return no_ingreso;
    }

    public int modificar(int id, string codigo, string nombres, string apellidos, string direccion, string telefono, string fecha_nacimiento, int id_puesto) {
      int no_ingreso = 0;
      conectar = new ConexionBD();
      conectar.AbrirConexion();

      string strConsulta = string.Format("UPDATE empleados SET codigo='{0}', nombres='{1}', apellidos='{2}', direccion='{3}', telefono='{4}', fecha_nacimiento='{5}', id_puesto={6} WHERE id_empleado={7};", codigo, nombres, apellidos, direccion, telefono, fecha_nacimiento, id_puesto, id);
      MySqlCommand modificar = new MySqlCommand(strConsulta, conectar.conectar);
      modificar.Connection = conectar.conectar;
      no_ingreso = modificar.ExecuteNonQuery();
      conectar.CerarConexion();
      return no_ingreso;
    }

    public int eliminar(int id) {
      int no_ingreso = 0;
      conectar = new ConexionBD();
      conectar.AbrirConexion();

      string strConsulta = string.Format("DELETE FROM empleados WHERE id_empleado={0};", id);
      MySqlCommand eliminar = new MySqlCommand(strConsulta, conectar.conectar);
      eliminar.Connection = conectar.conectar;
      no_ingreso = eliminar.ExecuteNonQuery();
      conectar.CerarConexion();
      return no_ingreso;
    }
  }
}
