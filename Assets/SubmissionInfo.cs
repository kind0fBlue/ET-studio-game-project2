
// This class contains metadata for your submission. It plugs into some of our
// grading tools to extract your game/team details. Ensure all Gradescope tests
// pass when submitting, as these do some basic checks of this file.
public static class SubmissionInfo
{
    // TASK: Fill out all team + team member details below by replacing the
    // content of the strings. Also ensure you read the specification carefully
    // for extra details related to use of this file.

    // URL to your group's project 2 repository on GitHub.
    public static readonly string RepoURL = "https://github.com/COMP30019/project-2-electronic-trash-studios-et";
    
    // Come up with a team name below (plain text, no more than 50 chars).
    public static readonly string TeamName = "Electronic Trash Studios (ET)";
    
    // List every team member below. Ensure student names/emails match official
    // UniMelb records exactly (e.g. avoid nicknames or aliases).
    public static readonly TeamMember[] Team = new[]
    {
        new TeamMember("Liangdongfang Xu", "liangdongfan@student.unimelb.edu.au"),
        new TeamMember("Chengyi Huang", "chengyih@student.unimelb.edu.au"),
        new TeamMember("Hecheng Yin", "hechengy@student.unimelb.edu.au"),
        new TeamMember("Jinbiao Wang", "jinbwang@student.unimelb.edu.au"), 
    };

    // This may be a "working title" to begin with, but ensure it is final by
    // the video milestone deadline (plain text, no more than 50 chars).
    public static readonly string GameName = "BOOM";

    // Write a brief blurb of your game, no more than 200 words. Again, ensure
    // this is final by the video milestone deadline.
    public static readonly string GameBlurb = 
@"The game we planned to design is a single player 3D third person view action game in the theme of 
monster apocalypse.  The controllable character is able to make damage to mobs through either melee 
attacks or shooting while the mobs are going to be monsters that do melee damage. The objective of the 
game is to survive under the waves of monster attacks for as long as possible to achieve a high score. 
There could potentially be a story mode where the player progresses through levels. As the game 
progresses, the difficulty of the game increases accordingly by generating more powerful mobs, the 
player will also be fighting in different scenes under different levels. We will also introduce RNG to the 
game, there will be props spawning randomly in the scene (extra health, grenade etc.) that the player 
can pick up for their advantage. In this project we will demonstrate our skill in shading and texture, 
hit boxes and character movement.
";
    
    // By the gameplay video milestone deadline this should be a direct link
    // to a YouTube video upload containing your video. Ensure "Made for kids"
    // is turned off in the video settings. 
    public static readonly string GameplayVideo = "https://www.youtube.com/watch?v=TrKG_ZqR2Gg";
    
    // No more info to fill out!
    // Please don't modify anything below here.
    public readonly struct TeamMember
    {
        public TeamMember(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }
        public string Email { get; }
    }
}
