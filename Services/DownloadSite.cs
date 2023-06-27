using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WSC.Services
{
    public class DownloadSite : IDownloadSite
    {
        private HtmlAgilityPack.HtmlDocument _htmlDoc;
        private string _url;
        private HttpClient _httpClient;
        private string _destination;

        private bool flag = true;

        private string _linksDirectory;
        private List<string> _distinctList;
        public DownloadSite(HttpClient httpClient, HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            _htmlDoc = htmlDoc;
            _httpClient = httpClient;
            _distinctList = new List<string>();
        }
        public void ClearList()
        {
            _distinctList.Clear();
        }
        public async Task DownloadAsync(string url, string destinationToDownload, string linksDirectory)
        {
            _url = url;
            _destination = destinationToDownload;
            _linksDirectory = linksDirectory;
            var rawHtml = await _httpClient.GetStringAsync(url);
            var filePath = "";
            _htmlDoc = new HtmlAgilityPack.HtmlDocument();
            _htmlDoc.LoadHtml(rawHtml);
            var hashTags = ProvideTag.GetAllTags();
            foreach (var tag in hashTags.Keys)
            {
                await DownloadTag(tag, hashTags[tag]);
            }
            if (flag)
            {
                filePath = Path.Combine(Path.GetDirectoryName(destinationToDownload), Path.GetFileNameWithoutExtension(_url) + ".html");
                flag = false;
            }
            else
            {
                Uri uri = new Uri(_url);
                string relativePath = uri.LocalPath;

                var baseFileName = RemoveSpecialCharacters(relativePath);
                filePath = _linksDirectory + "\\" + (baseFileName) + ".html";
            }

            File.WriteAllText(filePath, _htmlDoc.DocumentNode.OuterHtml);

        }

        private async Task DownloadTag(string tag, string tagAttr)
        {
            var generalNode = _htmlDoc.DocumentNode.SelectSingleNode("//html");
            var currentNode = generalNode.SelectNodes(tag);
            if (currentNode != null)
            {
                foreach (var node in currentNode)
                {
                    var src = node.GetAttributeValue(tagAttr, String.Empty);

                    var absoluteUrl = GetAbsolutePath(src, _url);
                    if (!string.IsNullOrEmpty(src) && IsHttp(absoluteUrl))
                    {
                        var response = await _httpClient.GetAsync(absoluteUrl);
                        if (!response.IsSuccessStatusCode)
                        {
                            //throw new Exception();
                        }
                        var content = await response.Content.ReadAsByteArrayAsync();
                        var fileName = "";
                        if (String.Equals("", fileName))
                        {
                            if (node.Name == "a")
                            {
                                fileName = RemoveSpecialCharacters(src) + ".html";

                                node.SetAttributeValue(tagAttr, _linksDirectory + "\\" + fileName);
                            }
                            else
                            {
                                if (node.Name == "link" && src.Contains(".css"))
                                {
                                    int extensionIndex = src.IndexOf(".css");
                                    fileName = src.Substring(0, extensionIndex);
                                    fileName = RemoveSpecialCharacters(fileName) + ".css";
                                    node.SetAttributeValue(tagAttr, _destination + "\\" + fileName);
                                }
                                else
                                {
                                    fileName = RemoveSpecialCharacters(src);
                                    node.SetAttributeValue(tagAttr, _destination + "\\" + fileName);
                                }

                            }

                        }
                        else
                        {
                            fileName = RemoveSpecialCharacters(src);
                            node.SetAttributeValue(tagAttr, _destination + "\\" + fileName);

                        }
                        if (content != null && content.Length > 0)
                        {
                            var filePath = Path.Combine(_destination, _destination + "\\" + fileName);
                            try
                            {
                                await File.WriteAllBytesAsync(filePath, content);
                            }
                            catch 
                            {

                            }
                        }

                    }

                }

            }
        }
        public static string RemoveSpecialCharacters(string input)
        {
            string[] specialChars = { "/", "\\", ":", "?", "*", "\"", "<", ">", "|" };

            foreach (string specialChar in specialChars)
            {
                input = input.Replace(specialChar, "");
            }

            return input;
        }

        private bool IsHttp(string absoluteUrl)
        {
            Uri uriResult;
            bool result = (Uri.TryCreate(absoluteUrl, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps));
            if (result != true)
            {

            }

            return result;
        }

        public async Task<List<string>> GetTheLinks(string url, int subNodeCount)
        {
            _url = url;
            var rawHtml = await _httpClient.GetStringAsync(url);
            _htmlDoc.LoadHtml(rawHtml);
            var links = new List<string>();
            foreach (HtmlNode link in _htmlDoc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string hrefValue = link.Attributes["href"].Value;

                if (subNodeCount == 0)
                    break;

                if (hrefValue.StartsWith("http://") || hrefValue.StartsWith("https://") || hrefValue.StartsWith("/"))
                {
                    var basePath = GetAbsolutePath(hrefValue, _url);
                    string basePathDomain = new Uri(basePath).Host;
                    string urlDomain = new Uri(url).Host;
                    if (basePathDomain == urlDomain)
                    {
                        if (!links.Contains(basePath))
                        {
                            if (!links.Contains(basePath) && !_distinctList.Contains(basePath))
                            {
                                _distinctList.Add(basePath);
                                links.Add(basePath);
                                subNodeCount--;
                            }
                        }
                    }
                }
            }

            return links;
        }



        private string GetAbsolutePath(string relativePath, string baseUrl)
        {
            Uri baseUri = new Uri(baseUrl);
            Uri absoluteUri = new Uri(baseUri, relativePath);
            if (absoluteUri.AbsoluteUri.EndsWith('/'))
            {
                return absoluteUri.AbsoluteUri.Substring(0, absoluteUri.AbsoluteUri.Length - 1);
            }
            return absoluteUri.AbsoluteUri;
        }

    }
}
