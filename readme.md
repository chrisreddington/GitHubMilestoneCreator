# GitHub Milestone Creator Tool

GitHubMilestoneCreator is a .NET Core command line script used to create milestones in GitHub issues based on Month.

In Azure DevOps, I had previously created a script to create a number of iterations based on months across different teams and projects. As I am managing my podcast through GitHub issues, I wanted to achieve something similar and automated, which set me on the path to creating this script.

## Installation

For the time being, please clone this repository and built the software manually. If there is a lot of interest, then I will look into a more formal release process.

## Usage

This is a .NET core console application, which can be executed using `dotnet GitHubMilestoneCreator.dll` once compiled. Please find below a list of the available arguments:

| Name                  | Flag                  | Required? | Description                                                                                                                                                                                                 |
|-----------------------|-----------------------|-----------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Personal Access Token | --token / -t          | Required  | GitHub Personal Access Token used to Authenticate to the GitHub APIs. More information on scopes can be found [here](https://docs.github.com/en/free-pro-team@latest/developers/apps/scopes-for-oauth-apps) |
| Organization          | --organization / -o   | Required  | The GitHub Organization or GitHub Account to which the repository belongs. For example, chrisreddington is the account in chrisreddington/cloudwithchris.com.                                               |
| Repository            | --repository / -r     | Required  | The name of the repository. For example, cloudwithchris.com is the repository in chrisreddington/cloudwithchris.com.                                                                                        |
| Number of Months      | --number-of-months / -m | Optional  | Number of months after the current month (not including current month) to create as individual milestones in the GitHub repository                                                                          |

## Authors and Acknowledgement

This creator tool leverages [GitHub's octokit.net library](https://github.com/octokit/octokit.net) as well as [Nate McMaster's CommandLineUtils](https://github.com/natemcmaster/CommandLineUtils) package.

## License

MIT License

Copyright (c) 2020 Chris Reddington

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.