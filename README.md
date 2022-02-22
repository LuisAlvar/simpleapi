# simpleapi
Azure Devops Example: deploying a webapi template for future reference
# Azure Devops: Continuous Deployment (Maunally Version)
Assumption(s)
* A functional .NET Core webapi application committed to GitHub 
* A functional .NET Core webapi with an Xunit project committed to GitHub
* Access **Azure DevOps Portal** create a new project and follow the required steps to create a new project. 
Afterwards, go to **Azure DevOps Portal** > **PipeLines**, create a new pipline follow and select Github as your codebase repo. 
For this project, follow the authentication and authroizations steps for Azure to access your Github account. 
The following page, Azure will provide a list of all the repos under your name. Select your project. 
Final step, you can select template for your azure-pipeline.yaml file. For this project, I selected ASP.NET as the template. 

```yaml
trigger:
- main

pool:
  vmImage: 'windows-latest'

variables:
  solution: '**/*.sln'
  buildPlatform: 'Any CPU'
  buildConfiguration: 'Release'

steps:
- task: NuGetToolInstaller@1

- task: NuGetCommand@2
  inputs:
    restoreSolution: '$(solution)'

- task: VSBuild@1
  inputs:
    solution: '$(solution)'
    msbuildArgs: '/p:DeployOnBuild=true /p:WebPublishMethod=Package /p:PackageAsSingleFile=true /p:SkipInvalidConfigurations=true /p:PackageLocation="$(build.artifactStagingDirectory)"'
    platform: '$(buildPlatform)'
    configuration: '$(buildConfiguration)'

- task: VSTest@2
  inputs:
    platform: '$(buildPlatform)'
    configuration: '$(buildConfiguration)'
```

# Azure Pipeline To Perform Xunit testing 

Azure Devops will run the configured pipeline, and commit the azure-pipeline file to your repo. 
If successful, then we add our xunit testing to the pipeline. Follow the instruction on this site [https://docs.microsoft.com/en-us/azure/devops/pipelines/ecosystems/dotnet-core?view=azure-devops&tabs=dotnetfive]. Edit for xunit testing local on your Visual Studio Code window. Then, follow the basic git command steps to push your changes to 
GitHub. 

```yaml
- task: DotNetCoreCLI@2
  inputs: 
    command: test
    projects: '**/*Test/*.csproj'
    arguments: '--configuration $(buildConfiguration)'
```

```bash
git add . 
git commit -m "add/mod xunit test case #3242345"
git push origin main
```

Go back to your Azure DevOps portal and you should see the pipleine running. 
Once its done inspect the DotNetCoreCLI step and view that all your tests passed in green. 

# Package Solution For Deployment 
Follow the same site [https://docs.microsoft.com/en-us/azure/devops/pipelines/ecosystems/dotnet-core?view=azure-devops&tabs=dotnetfive]
Next add the following to your azure-pipeline.yaml file

```yaml
- task: DotNetCoreCLI@2
  displayName: 'dotnet publish --configuration $(buildConfiguration) --output $(Build.ArtifactStagingDirectory)'
  inputs:
    command: publish
    publishWebProjects: false
    projects: 'src/SimpleAPI/SimpleAPI.csproj'
    arguments: '--configuration $(buildConfiguration) --output $(Build.ArtifactStagingDirectory)'
    zipAfterPublish: true 

- task: PublishBuildArtifacts@1
  displayName: 'publish artifacts'
```
Push all changes to git. 

# Azure: API App: Service 
Create an API App serivce: make sure you select the DEV/TEST F1 service tier.
Next, once API App service is deploy. Go to **Azure DevOps** > **Pipelines** > **Releases**.
Create a new release pipeline, go through the process and rename the stage. 
Under the main stage, select the job agent to be Azure App Service Deploy. 
Select your Azure subscription, under App Service type: API App, App Service name of your webapi site, resource group the check your Azure portal for such information
if its not auto populated already, hit save. 
Under Artifacts, select > LuisAlvar.simpleapi, latest for version and save. 
Next to your artifcat there is an icon, hit on it and enable **Continuous deployment trigger**
Final test, make a change to your project, then push the changes. 
Expections are the the git push will tigger the azure pipeline to start, and then release pipeline will kick-off. 
- If successfully, then you can access your https://[yourazuresite].net/WeatherForecast api and data should appear on your window. 

This template was built by the help of Lee Jackson, youtube walkthrough, https://www.youtube.com/watch?v=SOtC1VLZKm4&t=28


