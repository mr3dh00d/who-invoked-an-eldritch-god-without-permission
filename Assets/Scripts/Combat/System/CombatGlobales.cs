using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTypes
{
    public const string healthGreen = "#6BD924";
    public const string healthYellow = "#FFE900";
    public const string healthRed = "#FF1C00";
    public const string healthNone = "#00000000";

}

public class PollitoTypes
{
    public const string pollito = "Pollito";
    public const string hojita = "Hojita";
    public const string miope = "Miope";
    public const string payaso = "Payaso";
    public const string lloron = "Lloron";

}

public class ActionTypes
{
    public const string attack = "Atacar";
    public const string defense = "Defenderse";
    public const string item = "Objeto";
}

public class AttackTypes
{
    public const string basic = "Golpe Basico";

    public const string enemy = "Oscuridad";

    public static Attack getHeavyAttack(string name)
    {
        switch (name)
        {
            case PollitoTypes.hojita:
                return new Attack("Hojita de Papel", 10, 3, 2);
            case PollitoTypes.miope:
                return new Attack("Vista de Aguila", 10, 3, 2);
            case PollitoTypes.payaso:
                return new Attack("Chiste Racista", 10, Mathf.Infinity, 2);
            case PollitoTypes.lloron:
                return new Attack("Lagrimas de Cocodrilo", 10, 3, 2);
            default:
                return new Attack("Golpe Pesado", 10, 3, 2);
        }
    }

    public static Attack getPowerfulAttack(string name)
    {
        switch (name)
        {
            case PollitoTypes.hojita:
                return new Attack("Ranitas Unidas", 30, 2, 3);
            case PollitoTypes.miope:
                return new Attack("Pullitous Hyatus Promus", 30, 2, 3);
            case PollitoTypes.payaso:
                return new Attack("Lluvia de Confeti", 30, 1, 3);
            case PollitoTypes.lloron:
                return new Attack("Ataque Colorido", 30, 2, 3);
            default:
                return new Attack("Golpe Poderoso", 30, 2, 3);
        }
    }

    public static string getBasicAttackPhrase(Fighter attacker, Attack attack, Fighter target)
    {
        switch (attacker.name)
        {
            case PollitoTypes.hojita:
                switch (Random.Range(0,3))
                {
                    case 0:
                        return $"*{PollitoTypes.pollito} {attacker.name} salta como rana*\nPor la confusion {target.name} recibe {attack.damage} de daño.";
                    case 1:
                        return $"{PollitoTypes.pollito} {attacker.name} se lanza como un misil hacia {target.name} y recibe {attack.damage} de daño.";
                    case 2:
                        return $"{PollitoTypes.pollito} {attacker.name} presenta a su ranita mascota.\n{target.name} se acerca y la ranita le lame el ojo.\n{target.name} recibe {attack.damage} de daño por ranas.";
                    default:
                        break;
                }
                break;
            case PollitoTypes.miope:
                switch (Random.Range(0,3))
                {
                    case 0:
                        return $"{PollitoTypes.pollito} {attacker.name}: ¿Hay alguien ahí?\n{target.name} se siente ignorado y recibe {attack.damage} de daño.";
                    case 1:
                        return $"{PollitoTypes.pollito} {attacker.name} se tropieza con una piedra y se cae sobre {target.name}.\n{target.name} recibe {attack.damage} de daño.";
                    case 2:
                        return $"{PollitoTypes.pollito} {attacker.name} piensa que {target.name} es su abuelita y lo abraza tan fuerte que recibe {attack.damage} de amor dañino.";
                    default:
                        break;
                }
                break;
            case PollitoTypes.payaso:
                switch (Random.Range(0,3))
                {
                    case 0:
                        return $"{PollitoTypes.pollito} {attacker.name} intento distraer a {target.name} con malabares pero se resbalo y recibe un pelotazo de {attack.damage} de daño.";
                    case 1:
                        return $"{PollitoTypes.pollito} {attacker.name}: \"¿Sabes qué tienen en comun mis golpes y mis chistes? Ambos te dejarán en el suelo, llorando de dolor o de risa.\"\n{target.name} recibe {attack.damage} de daño";
                    case 2:
                        return $"{PollitoTypes.pollito} {attacker.name} cuenta un chiste tan malo que {target.name} recibe {attack.damage} de daño en risa.";
                    default:
                        break;
                }
                break;
            case PollitoTypes.lloron:
                switch (Random.Range(0,3))
                {
                    case 0:
                        return $"*{PollitoTypes.pollito} {attacker.name} se pone a llorar*\n{attacker.name} llora tan fuerte que a {target.name} le dio pena y recibe {attack.damage} de daño emocional.";
                    case 1:
                        return $"{PollitoTypes.pollito} {attacker.name} le cuenta sus problemas a {target.name} y este se siente tan mal que recibe {attack.damage} de daño emocional.";
                    case 2:
                        return $"{PollitoTypes.pollito} {attacker.name} piensa que {target.name} es su ex y le reclama por todo lo que le hizo.\n{target.name} recibe {attack.damage} de daño emocional.";
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        return "";
    }
    public static string getHeavyAttackPhrase(Fighter attacker, Attack attack, Fighter target)
    {
        switch (attacker.name)
        {
            case PollitoTypes.hojita:
                return $"{PollitoTypes.pollito} {attacker.name} escribe en una hoja de papel \"-10\" y la arroja a {target.name}.\nPor alguna razón funciona y {target.name} recibe 10 de daño.";
            case PollitoTypes.miope:
                return $"{PollitoTypes.pollito} {attacker.name} fuerza la vista y ve a {target.name}.\n{target.name} se siente intimidado y recibe {attack.damage} de daño.";
            case PollitoTypes.payaso:
                return $"{PollitoTypes.pollito} {attacker.name}: \"¿Crees que el racismo es chistoso?\"";
            case PollitoTypes.lloron:
                return $"{PollitoTypes.pollito} {attacker.name} derrama lágrimas engañosas.\n{target.name} se conmueve y piensa \"¿Cuál es su problema?\".\nDe repente, ¡una lágrima le golpea en la nariz y recibe {attack.damage} de daño!\n¡Un llanto impactante y pio-tente!";
            default:
                break;
        }
        return "";
    }

    public static string getPowerfulAttackPhrase(Fighter attacker, Attack attack, Fighter target)
    {
        switch (attacker.name)
        {
            case PollitoTypes.hojita:
                return $"{PollitoTypes.pollito} {attacker.name} llama a todas las ranitas cercanas.\nLas ranitas se unen y forman una ranita gigante y golpean a {target.name}.\nRecibe {attack.damage} de daño";
            case PollitoTypes.miope:
                return $"{PollitoTypes.pollito} {attacker.name}: \"Pullitous Hyatus Promus, crom-brius pullorex! Kikiri-pio, kikiri-pio! Añarol plumitus, rumix korix eternus. Pulloitus parrandius, pluma no morix nunca.\"\nA {target.name} le cae un elefante encima y recibe {attack.damage} de daño.";
            case PollitoTypes.payaso:
                return $"{PollitoTypes.pollito} {attacker.name} saca su diminuto cañon de confeti.\n{target.name} se rie.\n{attacker.name} dispara el cañon y. . . . . . . . . . . . . . . . . . .\nSolo salen 3 papelitos.\n{target.name} recibe {attack.damage} de daño.";
            case PollitoTypes.lloron:
                return $"{PollitoTypes.pollito} {attacker.name} saca un atril y un bastidor, y comienza a retratar a {target.name}.\n{target.name} posa para salir bien en la pintura.\n(30 minutos despues)\nEs solo un cuadro con manchas rojas. {attacker.name} olvido que era daltonico asi que agarra la pintura y le pega a {target.name}.\nRecibe {attack.damage} de daño.";
            default:
                break;
        }
        return "";
    }

    public static string attackMessage(Fighter attacker, Attack attack, Fighter target)
    {
        string message;
        switch (attack.level)
        {
            case 1:
                message = getBasicAttackPhrase(attacker, attack, target);
                break;
            case 2:
                message = getHeavyAttackPhrase(attacker, attack, target);
                break;
            case 3:
                message = getPowerfulAttackPhrase(attacker, attack, target);
                break;
            default:
                message = "No se encontro el ataque";
                break;
        }
        if (message != "")
        {
            return message;
        }

        return $"{attacker.name} ha usado {attack.name} contra {target.name} y recibe {attack.damage} de daño.";
    }
}

public class DefenseTypes
{
    public static string defenseMessage(Fighter attacker)
    {
        switch (attacker.name)
        {
            case PollitoTypes.hojita:
                return $"{attacker.name} se ha cubierto de hojitas.\n+{attacker.stats.getDefense()} de defensa.";
            case PollitoTypes.miope:
                return $"{attacker.name} se ha puesto sus lentes.\n+{attacker.stats.getDefense()} de defensa.";
            case PollitoTypes.payaso:
                return $"{attacker.name} se ha puesto su nariz de payaso.\n+{attacker.stats.getDefense()} de defensa.";
            case PollitoTypes.lloron:
                return $"{attacker.name} se ha puesto a llorar.\n+{attacker.stats.getDefense()} de defensa.";
        }
        return $"{attacker.name} ha entrado en modo defensa.";
    }
}