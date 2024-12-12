using System;

public abstract class Decision {
    public abstract AgentCommand getDecision(AgentNav agent);
}

public class DecisionAction : Decision
{
    public AgentCommand command;

    public DecisionAction(AgentCommand command)
    {
        this.command = command;
    }

    public override AgentCommand getDecision(AgentNav agent)
    {
        command.setAgent(agent);
        return command;
    }
}


public class DecisionComposite : Decision
{
    // For use in a static map in case we want to reuse them to build other trees
    public string title;
    public Func<AgentNav, bool> trueFunc { get; set; }

    public Decision pos { get; set; }
    public Decision neg { get; set; }

    public override AgentCommand getDecision(AgentNav agent)
    {
        bool result = trueFunc(agent);

        return result ? 
            pos.getDecision(agent) : 
            neg.getDecision(agent);
    }
}