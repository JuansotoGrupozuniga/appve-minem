﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sres.be;
using sres.da;
using sres.ut;
using Oracle.DataAccess.Client;
using System.Data;

namespace sres.ln
{
    public class UsuarioLN : BaseLN
    {
        UsuarioDA usuarioDA = new UsuarioDA();

        public List<UsuarioBE> ListaUsuario()
        {
            List<UsuarioBE> lista = new List<UsuarioBE>();

            try
            {
                cn.Open();
                lista = usuarioDA.ListaUsuario(cn);
            }
            catch (Exception ex) { Log.Error(ex); }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return lista;
        }

        public UsuarioBE ObtenerUsuarioPorCorreo(string correo)
        {
            UsuarioBE item = null;

            try
            {
                cn.Open();
                item = usuarioDA.ObtenerUsuarioPorCorreo(correo, cn);
            }
            catch (Exception ex) { Log.Error(ex); }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return item;
        }

        public List<UsuarioBE> BuscarUsuario(string busqueda, int registros, int pagina, string columna, string orden)
        {
            List<UsuarioBE> lista = new List<UsuarioBE>();

            try
            {
                cn.Open();
                lista = usuarioDA.BuscarUsuario(busqueda, registros, pagina, columna, orden, cn);
            }
            catch (Exception ex) { Log.Error(ex); }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return lista;
        }

        public List<UsuarioBE> ListarUsuarioPorRol(int idRol)
        {
            List<UsuarioBE> lista = new List<UsuarioBE>();

            try
            {
                cn.Open();
                lista = usuarioDA.ListarUsuarioPorRol(idRol, cn);
            }
            catch (Exception ex) { Log.Error(ex); }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return lista;
        }

        public UsuarioBE ObtenerUsuario(int idUsuario)
        {
            UsuarioBE item = null;

            try
            {
                cn.Open();
                item = usuarioDA.ObtenerUsuario(idUsuario, cn);
            }
            catch (Exception ex) { Log.Error(ex); }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return item;
        }

        public UsuarioBE ObtenerUsuarioPorInstitucionCorreo(int idInstitucion, string correo)
        {
            UsuarioBE item = null;

            try
            {
                cn.Open();
                item = usuarioDA.ObtenerUsuarioPorInstitucionCorreo(idInstitucion, correo, cn);
            }
            catch (Exception ex) { Log.Error(ex); }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return item;
        }

        public bool GuardarUsuario(UsuarioBE usuario)
        {
            bool seGuardo = false;

            try
            {
                cn.Open();
                using (OracleTransaction ot = cn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {                    
                    if (usuario.ID_USUARIO <= 0 ) usuario.CONTRASENA = string.IsNullOrEmpty(usuario.CONTRASENA) ? null : Seguridad.hashSal(usuario.CONTRASENA);
                    seGuardo = usuarioDA.GuardarUsuario(usuario, cn);

                    if (seGuardo) ot.Commit();
                    else ot.Rollback();
                }
            }
            catch (Exception ex) { Log.Error(ex); }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }


            return seGuardo;
        }

        public bool ValidarUsuario(string correo, string contraseña, out UsuarioBE outUsuario)
        {
            outUsuario = null;

            bool esValido = false;

            try
            {
                cn.Open();
                outUsuario = usuarioDA.ObtenerUsuarioPorCorreo(correo, cn);
                esValido = outUsuario != null;
                if (esValido) esValido = Seguridad.CompararHashSal(contraseña, outUsuario.CONTRASENA);
            }
            catch (Exception ex) { Log.Error(ex); }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return esValido;
        }
        
        public List<UsuarioBE> getAllEvaluador()
        {
            List<UsuarioBE> lista = new List<UsuarioBE>();

            try
            {
                cn.Open();
                lista = usuarioDA.getAllEvaluador(cn);
            }
            catch (Exception ex) { Log.Error(ex); }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return lista;
        }

        public int CambiarClave(UsuarioBE usuario)
        {
            int estado = 0;
            bool cambio = false;
            try
            {
                cn.Open();
                UsuarioBE usu = usuarioDA.ObtenerClave(usuario.ID_USUARIO, cn);
                estado = usu != null ? 0 : 1;
                if (estado == 0) {
                    cambio = string.IsNullOrEmpty(usuario.CONTRASENA) ? true : Seguridad.CompararHashSal(usuario.CONTRASENA, usu.CONTRASENA);
                    estado = cambio ? 0 : 2;
                    if (estado == 0) {
                        usuario.CONTRASENA_NUEVO = string.IsNullOrEmpty(usuario.CONTRASENA_NUEVO) ? null : Seguridad.hashSal(usuario.CONTRASENA_NUEVO);
                        estado = usuario.CONTRASENA_NUEVO == null ? 1 : 0;
                        if (estado == 0) {
                            cambio = usuarioDA.CambiarClave(usuario.ID_USUARIO, usuario.CONTRASENA_NUEVO, cn);
                            estado = cambio ? 3 : 1;
                        }
                    }                    
                } 
            }
            catch (Exception ex) { Log.Error(ex); }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return estado;
        }

        //=====================

        public bool VerificarCorreo(string correo)
        {
            bool valor = false;

            try
            {
                cn.Open();
                valor = usuarioDA.VerificarCorreo(correo, cn);
            }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return valor;
        }

    }
}
