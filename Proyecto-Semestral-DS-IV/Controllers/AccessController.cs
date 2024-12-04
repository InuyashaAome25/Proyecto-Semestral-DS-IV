using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Proyecto_Semestral_DS_IV.Models.DAO;
using Proyecto_Semestral_DS_IV.Models.Modelos;

namespace Proyecto_Semestral_DS_IV.Controllers
{
    public class AccessController : Controller
    {
        private clsUsuarios usuarioDAO = new clsUsuarios();

        // GET: Access
        public ActionResult Login()
        {
            return View();
        }

        // POST: Access/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = usuarioDAO.ValidarUsuario(model.UserName, model.Password);
                if (usuario != null)
                {
                    // Establecer la sesión
                    Session["UserID"] = usuario.IDUsuario;
                    Session["UserName"] = usuario.UserName;
                    Session["RolID"] = usuario.RolID;

                    // Crear ticket de autenticación
                    FormsAuthentication.SetAuthCookie(usuario.UserName, false);

                    if (usuario.RolID == 1) // Usuario
                    {
                        return RedirectToAction("Inicio", "Home");
                    }
                    else if (usuario.RolID == 2) // Administrador
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                }
                else
                {
                    ViewBag.Mensaje = "Credenciales inválidas.";
                }
            }
            return View(model);
        }

        public ActionResult Registrar()
        {
            return View();
        }

        // POST: Access/Registrar
        [HttpPost]
        public ActionResult Registrar(Usuario objUsuario)
        {
            if (ModelState.IsValid)
            {
                string mensaje;
                bool success = usuarioDAO.CrearUsuario(objUsuario.Nombre, objUsuario.Apellido, objUsuario.Correo, objUsuario.Telefono, objUsuario.Direccion, objUsuario.UserName, objUsuario.PasswordUser, out mensaje);

                ViewBag.Mensaje = mensaje;

                if (success)
                {
                    return RedirectToAction("Login"); // Redirige al login tras un registro exitoso
                }
            }
            return View(objUsuario);
        }

        // GET: Access/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}