using Mafia.Engine.Models;

namespace Mafia.Engine.Test;

public class GodFatherScenarioTest
{
    private MafiaInstance _instance { get; set; }
    
    [SetUp]
    public void Setup()
    {
         _instance = new MafiaInstance(11, 11, true);

        _instance.AddRole(new Roles("God Father", RoleSide.Mafia,true));
        _instance.AddRole(new Roles("Saul Goodman", RoleSide.Mafia,true));
        _instance.AddRole(new Roles("Matador", RoleSide.Mafia,true));

        _instance.AddRole(new Roles("Nostradamus", RoleSide.None,true));

        _instance.AddRole(new Roles("Dr. Watson", RoleSide.Citizen,true));
        _instance.AddRole(new Roles("Leon The Professional", RoleSide.Citizen,true));
        _instance.AddRole(new Roles("Citizen kane", RoleSide.Citizen,true));
        _instance.AddRole(new Roles("Constantine", RoleSide.Citizen,true));
        _instance.AddRole(new Roles("Citizen", RoleSide.Citizen,false));

        _instance.AddUser(new Users("Akbar Ahmadi"));
        _instance.AddUser(new Users("Hadi Ahmadi"));
        _instance.AddUser(new Users("Ali Ahmadi"));
        _instance.AddUser(new Users("Mojtaba Ali Mohamadi"));
        _instance.AddUser(new Users("Leila Sadegi"));
        _instance.AddUser(new Users("Fatemeh Ahmadi"));
        _instance.AddUser(new Users("Zahra Ahmadi"));
        _instance.AddUser(new Users("Nosrat Ahmadi"));
        _instance.AddUser(new Users("AmirHosein Yagooti"));
        _instance.AddUser(new Users("Raziyeh Abbasi"));
        _instance.AddUser(new Users("Saedeh Abbasi"));
    }

    [Test]
    public void ShuffleRolesTest()
    {
        var engine = new MafiaEngine(_instance);
        engine.ShuffleRoles();
        engine.Execute();
    }
}