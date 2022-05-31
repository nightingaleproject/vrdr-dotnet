using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json.Linq;

namespace VRDR.HTTP
{
    public class VRDRListener
    {
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerRequest, string> _responderMethod;

        public VRDRListener(string[] prefixes, Func<HttpListenerRequest, string> method)
        {
            // URI prefixes are required, for example
            // "http://localhost:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
            {
                throw new ArgumentException("prefixes");
            }

            // A responder method is required
            if (method == null)
            {
                throw new ArgumentException("method");
            }

            foreach (string s in prefixes)
            {
                _listener.Prefixes.Add(s);
            }

            _responderMethod = method;
            _listener.Start();
        }

         public VRDRListener(Func<HttpListenerRequest, string> method, params string[] prefixes)
            : this(prefixes, method) { }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                Console.WriteLine("FHIR Death Record Translation Service Listener Running...");
                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem((c) =>
                        {
                            var ctx = c as HttpListenerContext;
                            try
                            {
                                string rstr = _responderMethod(ctx.Request);
                                byte[] buf = Encoding.UTF8.GetBytes(rstr);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.ContentType = "application/json";
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            catch (Exception e)
                            {
                                string rstr = e.Message;
                                string response = Program.GenerateJsonResponse("false", ResponseTypes.Error.ToString(), rstr);
                                byte[] buf = Encoding.UTF8.GetBytes(response);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.ContentType = "application/json";
                                ctx.Response.StatusCode = 400;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            finally
                            {
                                ctx.Response.OutputStream.Close();
                            }
                        }, _listener.GetContext());
                    }
                }
                catch {}
            });
        }

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}