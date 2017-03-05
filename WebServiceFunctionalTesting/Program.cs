using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebServiceFunctionalTesting
{
	class Program
	{
		static void Main(string[] args)
		{
			WebRequest request = WebRequest.Create("http://ninewebservice.apphb.com/");
			request.Credentials = CredentialCache.DefaultCredentials;
			request.Method = "POST";
			//string requestData = File.ReadAllText(@"TestData.json");
			string requestData = "nonsense";
			byte[] requestBytes = Encoding.UTF8.GetBytes(requestData);
			request.ContentLength = requestBytes.Length;
			request.GetRequestStream().Write(requestBytes, 0, requestBytes.Length);
			request.GetRequestStream().Close();
			WebResponse response;
			try
			{
				response = request.GetResponse();
				Console.WriteLine(((HttpWebResponse)response).StatusCode);
				byte[] responseText = new byte[response.ContentLength];
				Stream dataStream = response.GetResponseStream();
				dataStream.Read(responseText, 0, (int)response.ContentLength);
				File.WriteAllBytes(@"Output.json", responseText);
				response.Close();
				Console.ReadKey();
			}
			catch (WebException exception)
			{
				response = exception.Response;
				Console.WriteLine(((HttpWebResponse)response).StatusCode);
				byte[] responseText = new byte[response.ContentLength];
				Stream dataStream = response.GetResponseStream();
				dataStream.Read(responseText, 0, (int)response.ContentLength);
				File.WriteAllBytes(@"Output.json", responseText);
				response.Close();
				Console.ReadKey();
			}
		}
	}
}
