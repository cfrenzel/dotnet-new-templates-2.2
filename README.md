=======


**Create Template Pack**

<pre>
> dotnet pack .\templates\templates.csproj -o .\artifacts\ --no-build --no-restore
</pre>

**Install Templates Locally

<pre>
> dotnet new -i .\artifacts\cfrenzel-dotnet-new-templates-2.2.1.0.0.nupkg
</pre>

**Install from MyGet**

<pre>
 dotnet new --install cfrenzel-dotnet-new-templates-2.2  --nuget-source https://www.myget.org/F/cfrenzel-ci/api/v3/index.json
</pre>

