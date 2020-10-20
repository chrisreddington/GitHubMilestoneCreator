
using McMaster.Extensions.CommandLineUtils;
using Octokit;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace GitHubMilestoneCreator
{
    public class Program
    {
        
        [Required]
        [Option(
            Description = "GitHub Personal Access Token to authenticate the script",
            ShortName = "t"
        )]
        public static string token { get; set; }

        [Required]
        [Option(
            Description = "GitHub Account Owner, or Organization, e.g. chrisreddington chrisreddington/cloudwithchris.com",
            ShortName = "o"
        )]
        public static string organization { get; set; }

        [Required]
        [Option(
            Description = "Repository Name, e.g. cloudwithchris.com chrisreddington/cloudwithchris.com",
            ShortName = "r"
        )]
        public static string repository { get; set; }

        [Option(
            Description = "Number of months after the current month (not including current month) to create as individual milestones in the GitHub repository",
            ShortName = "m"
        )]
        public static int numberOfMonths { get; set; } = 3;
        
        public static int Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        private async Task OnExecute()
        {            
            var productInformation = new ProductHeaderValue(repository);
            var credentials = new Credentials(token);
            var github = new GitHubClient(productInformation) { Credentials = credentials };

            GitHubHelper gitHubHelper = new GitHubHelper(github);

            await gitHubHelper.GetRepository(organization, repository);
            await gitHubHelper.GetUpcomingMonths(numberOfMonths);
        }
    }

    public class GitHubHelper {

        public GitHubHelper (GitHubClient _client){
            github = _client;
        }

        private GitHubClient github;
        private long repositoryId;
                

        public async Task GetRepository(string organization, string repositoryName){
            Repository repository = await github.Repository.Get(organization, repositoryName);
            repositoryId = repository.Id;
            Console.WriteLine($"Successfully found Repo ID: {repositoryId}");
        }

        public async Task GetUpcomingMonths(int numberOfMonths){
            DateTime currentDateTime = DateTime.Now;
            DateTime firstOfFutureMonth;
            DateTime lastDayOfFutureMonth;

            for (int i = 1; i <= numberOfMonths; i++){
                firstOfFutureMonth = new DateTime(currentDateTime.AddMonths(i).Year, currentDateTime.AddMonths(i).Month, 1, 12, 0, 0);
                lastDayOfFutureMonth = firstOfFutureMonth.AddMonths(1).AddDays(-1);
                await CreateMilestoneIfNotExists(lastDayOfFutureMonth);          
            }
        }

        public async Task CreateMilestoneIfNotExists(DateTime lastDayOfMonth){
            string milestoneTitle = lastDayOfMonth.ToString("MMM-yyyy");
            string milestoneDescription = $"This milestone shows the items for release in {lastDayOfMonth.ToString("MMMM")}-{lastDayOfMonth.Year}";

            NewMilestone newMilestone = new NewMilestone(milestoneTitle) {
                Description = milestoneDescription,
                DueOn = lastDayOfMonth
            };
            try {
                await github.Issue.Milestone.Create(repositoryId, newMilestone);
                Console.WriteLine($"Created {milestoneTitle}");
            } catch (Exception ex) {
                Console.WriteLine($"Skipping {milestoneTitle}");
            }
        }
    }
}