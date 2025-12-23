namespace ClinicaSR.PL.WebApp.Middleware
{
    public class SecurityConfigMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityConfigMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.Value?.ToLower() ?? "";
            string? usuarioID = context.Session.GetString("UsuarioID");
            string? usuarioRol = context.Session.GetString("UsuarioRol");

            // 1. Rutas Públicas e Imágenes (Importante incluir /images para tu logo/perfil)
            if (path == "/" || path.Contains("/seguridad/login") || path.Contains("/css") ||
                path.Contains("/js") || path.Contains("/images") || path.Contains("/lib"))
            {
                await _next(context);
                return;
            }

            // 2. Si no hay sesión, siempre al Login
            if (string.IsNullOrEmpty(usuarioID))
            {
                context.Response.Redirect("/Seguridad/Login");
                return;
            }

            // 3. Validación de Roles por Ruta Real
            bool tieneAcceso = true;

            // Bloqueo para Panel Administrador
            if (path.Contains("/home/paneladministrador") && usuarioRol != "ADMINISTRADOR")
                tieneAcceso = false;

            // Bloqueo para Panel Recepcionista
            else if (path.Contains("/home/panelrecepcionista") && usuarioRol != "RECEPCIONISTA")
                tieneAcceso = false;

            // Bloqueo para Panel Cajero
            else if (path.Contains("/home/panelcajero") && usuarioRol != "CAJERO")
                tieneAcceso = false;

            if (!tieneAcceso)
            {
                // Si intenta entrar a un panel que no le toca, lo mandamos a SU panel correcto
                if (usuarioRol == "ADMINISTRADOR") context.Response.Redirect("/Home/PanelAdministrador");
                else if (usuarioRol == "RECEPCIONISTA") context.Response.Redirect("/Home/PanelRecepcionista");
                else context.Response.Redirect("/Home/PanelCajero");
                return;
            }

            await _next(context);
        }

    }
}
