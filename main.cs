public string GetCustomers()
{
    const string url = "https://your-store.myshopify.com/admin/customers.json";
 
    var req = (HttpWebRequest)WebRequest.Create(url);
    req.Method = "GET";
    req.ContentType = "application/json";
    req.Credentials = GetCredential(url);
    req.PreAuthenticate = true;
 
    using (var resp = (HttpWebResponse)req.GetResponse())
    {
        if (resp.StatusCode != HttpStatusCode.OK)
        {
            string message = String.Format("Call failed. Received HTTP {0}", resp.StatusCode);
            throw new ApplicationException(message);
        }
 
        var sr = new StreamReader(resp.GetResponseStream());
        return sr.ReadToEnd();
    }
}
 
private static CredentialCache GetCredential(string url)
{
    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
    var credentialCache = new CredentialCache();
    credentialCache.Add(new Uri(url), "Basic", new NetworkCredential("your-api-key", "your-password"));
    return credentialCache;
}