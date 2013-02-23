using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using ClientServeur;
using LibrairieUtil;
using LibrairieUtil.AnimatedSprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TTRPG_case.Communication;
using TTRPG_case.Perso;
using Commun;
using Commun.Map;
using LibrairieMessagesContexte.Messages;

namespace TTRPG_case
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public static int TailleCaseX = 40;
        public static int TailleCaseY = 40;
        public static int NombreTickDeplacement = 20;
        public static int PortReception;
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        readonly Emetteur _emmeteur;
        readonly Recepteur _recepteur;
        readonly Vecteur[] _deplacementPerFrame;
        
        Personnage _personnage;
        private Dictionary<string, Personnage> _persoAutres;


        readonly Dictionary<string, Texture2D> _listeTextureCarte;

        //Test
        readonly Dictionary<string, CarteEcran> _listeCarte;
        CarteEcran _carteEcran ;

        //public event CarteRecueHandler CarteRecue;

        //public delegate void CarteRecueHandler(object sender, EventArgs ev);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            this._graphics.PreferredBackBufferHeight = 600;
            this._graphics.PreferredBackBufferWidth = 800;
            this._graphics.ApplyChanges();
            this.IsMouseVisible = true;
            IPAddress ip = GestionReseau.GetMyLocalIp();
            this._emmeteur = new Emetteur(ip.ToString(), Constantes.PORT_ENVOIE);
            PortReception = GestionReseau.TrouverPort(1500);
            this._recepteur = new Recepteur(PortReception, this);
            this._listeTextureCarte = new Dictionary<string, Texture2D>();
            this._listeCarte = new Dictionary<string, CarteEcran>();

            this._deplacementPerFrame = new Vecteur[4];
            this._deplacementPerFrame[0] = new Vecteur(0, TailleCaseY / NombreTickDeplacement);
            this._deplacementPerFrame[1] = new Vecteur(TailleCaseX / NombreTickDeplacement, 0);
            this._deplacementPerFrame[2] = Vecteur.Inverse(_deplacementPerFrame[0]);
            this._deplacementPerFrame[3] = Vecteur.Inverse(_deplacementPerFrame[1]);

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            this._recepteur.Initialiser();
            this._recepteur.LancerEcoute();
            this._emmeteur.Connecter();
            //envoyer meqsage port;
            IMessage messagePort = MessageFactory.GetInstanceOf(TypeMessage.IndiquerPort);
            messagePort.PreparerMessage(new object[] { PortReception });
            this._emmeteur.envoyer(messagePort);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            
            var te = this.Content.Load<Texture2D>("caseverte");
            var tevide = this.Content.Load<Texture2D>("casevide");


            #region creation sprite perso
            var spritehaut = new AnimatedSprite();
            var spritebas = new AnimatedSprite();
            var spritegauche = new AnimatedSprite();
            var spritedroite = new AnimatedSprite();
            for (var i = 0; i < 3; i++)
            {
                spritehaut.AjoutAnimationFrame(this.Content.Load<Texture2D>("AnimationMarche/haut" + i), 10);
                spritebas.AjoutAnimationFrame(this.Content.Load<Texture2D>("AnimationMarche/bas" + i), 10);
                spritegauche.AjoutAnimationFrame(this.Content.Load<Texture2D>("AnimationMarche/gauche" + i), 10);
                spritedroite.AjoutAnimationFrame(this.Content.Load<Texture2D>("AnimationMarche/droite" + i), 10);
            }

            this._personnage = new Personnage(spritehaut, spritebas, spritegauche, spritedroite, 10, 10);





            #endregion


            this._listeTextureCarte.Add("1", te);
            this._listeTextureCarte.Add("0", tevide);

            AfficherCarte(new ObjetChargementCarte(this, "cartevide"));
            var t = MessageFactory.GetInstanceOf(TypeMessage.DemandeCarte);
            t.PreparerMessage(new object[] { "test" });
            this._emmeteur.envoyer(t);

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState ms = Mouse.GetState();
            if (ms.X > 0 && ms.X < this._carteEcran.NombreCasesX * TailleCaseX && ms.Y > 0 && ms.Y < this._carteEcran.NombreCasesY * TailleCaseY && this.IsActive && !this._personnage.ACheminPrevu())
            {
                //Console.WriteLine("Souris dans la fenetre");
                //Console.WriteLine("Case en " + ms.X / TAILLE_CASE_X + " , " + ms.Y / TAILLE_CASE_Y);
                if (ms.LeftButton == ButtonState.Pressed && this._personnage.Compteur<0)
                {
                    //IMessage temp = MessageFactory.GetInstanceOf(TypeMessage.DemandeDeplacement);
                    var coo = new Coordonnees(ms.X / TailleCaseX, ms.Y / TailleCaseY);
                    var c = this._carteEcran.CalculerChemin(this._personnage.Coordonnees, coo);
                    //TODO : verifier chemin cot� serveur


                    this._personnage.CheminPerso = c;

                    IMessage m = MessageFactory.GetInstanceOf(TypeMessage.DemandeDeplacement);
                    //Vecteur t = this.personnage.getNextMouvement();
                    m.PreparerMessage(new object[] { c});
                    this._emmeteur.envoyer(m);


                }
            }

            if (this._personnage.Compteur > -1)
                this._personnage.Compteur--;


            if (this._personnage.Compteur < 0 && this._personnage.ACheminPrevu() )
            {
                Vecteur t = this._personnage.GetNextMouvement();
                this.DeplacementPerso(t);
                this._personnage.Compteur = NombreTickDeplacement;
                
            }
            if (this._personnage.ACheminPrevu() && this._personnage.Flagdepl)
            {
                this._personnage.Tick();

            }


            base.Update(gameTime);
        }


       
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            for (int x = 0; x < this._carteEcran.NombreCasesX; x++)
            {

                for (int y = 0; y < this._carteEcran.NombreCasesY; y++)
                {
                    _spriteBatch.Draw(this._carteEcran.GetCase(x, y).getImage(), new Rectangle(x * TailleCaseX, y * TailleCaseY, TailleCaseX, TailleCaseY), Color.White);
                }
            }

            _spriteBatch.Draw(this._personnage.GetSprite, new Rectangle(this._personnage.Coordonnees.X * TailleCaseX + this._personnage.OffsetCaseSprite.X + this._personnage.OffsetCaseDepl.vx, this._personnage.Coordonnees.Y * TailleCaseY + this._personnage.OffsetCaseSprite.Y + this._personnage.OffsetCaseDepl.vy, this._personnage.GetSprite.Width, this._personnage.GetSprite.Height), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }


        /// <summary>
        /// Lance l'affichage d'une carte de maniere thread�e
        /// </summary>
        /// <param name="nomCarte"></param>
        private void AfficherCarteThreadee(string nomCarte)
        {
            var oc = new ObjetChargementCarte(this, nomCarte);
            var t = new Thread(AfficherCarte);
            t.Start(oc);


        }

        /// <summary>
        /// Chaarge une carte sur l'afficheur
        /// </summary>
        /// <param name="oc"></param>
        private void AfficherCarte(object oc)
        {
            var o = (ObjetChargementCarte)oc;
            var nomcarte = o.CarteACharger;
            var g = o.G;

            if (g._listeCarte.ContainsKey(nomcarte))
            {
                g._carteEcran = this._listeCarte["nomcarte"];
            }
            else
            {
                var stream = TitleContainer.OpenStream("Cartes/" + nomcarte + ".txt");
                var sr = new StreamReader(stream);
                var temp = sr.ReadLine();
                var li = temp.Split(';');
                var tx = Int32.Parse(li[0]);
                var ty = Int32.Parse(li[1]);
                g._carteEcran = new CarteEcran(tx, ty);
                for (var y = 0; y < ty; y++)
                {
                    temp = sr.ReadLine();
                    li = temp.Split(';');
                    for (var x = 0; x < tx; x++)
                    {
                        g._carteEcran.InitialiserCase(x, y, g._listeTextureCarte[li[x]]);
                    }

                }
                g._listeCarte.Add(nomcarte, g._carteEcran);
            }
            Console.WriteLine("Carte : " + nomcarte + " chargee");

        }

        /// <summary>
        /// Classe de gestion de chargement des cartes
        /// </summary>
        private class ObjetChargementCarte
        {
            public readonly Game1 G;
            public readonly string CarteACharger;

            public ObjetChargementCarte(Game1 ga, string c)
            {
                this.G = ga;
                this.CarteACharger = c;
            }


        }

        /// <summary>
        /// Methode de IContexte, appell�e par l'execution d'un message demande carte
        /// </summary>
        /// <param name="valeur">valeur renvoy�e par le serveur</param>
        /// <param name="posX">Pisition X de depart</param>
        /// <param name="posY">position Y de depart</param>
        public void ReponseCarte(string valeur,int posX, int posY)
        {
            Console.WriteLine("Carte demandee en chargement : " + valeur);
            this.AfficherCarteThreadee(valeur);
            //on place le perso � sa position indiqu�e par le serveur
            this._personnage.Coordonnees = new Coordonnees { X = posX, Y = posY };
        }


        public void ReponseDeplacement(string p)
        {
            Console.WriteLine("Reponse re�ue du serveur pour le deplacement : " + p);
            var t = p.Split(';');

            if (t[0] == "true")
            {
                this._personnage.Flagdepl = true;
                this._personnage.Compteur = NombreTickDeplacement;

            }
            else
            {
                Console.WriteLine("Deplacement refus�");
                this._personnage.CheminPerso = null;
            }

        }

        private void DeplacementPerso(Vecteur v)
        {
            Console.WriteLine("Deplacement " + v);
            Console.WriteLine("Direction avant : " + this._personnage.Direction);
            
            //todo : optimiser
            switch (v.vx)
            {
                case -1:
                    this._personnage.Direction = 3;
                    break;
                case 1:
                    this._personnage.Direction = 1;
                    break;
                default:
                    switch (v.vy)
                    {
                        case -1:
                            this._personnage.Direction = 0;
                            break;
                        case 1:
                            this._personnage.Direction = 2;
                            break;
                    }
                    break;
            }

            this._personnage.Move(v);

            Console.WriteLine("Direction apres : " + this._personnage.Direction);
        }




        internal void NotifDepl(string joueurUi, string chemin)
        {
            Console.WriteLine("Deplacement de " + joueurUi +" sur le chemin "+chemin);
        }

        public void ConnexionNvxJoueur(string oidj, string skin)
        {
            Console.WriteLine("Joueur " + oidj + " connect�");
        }
    }

   
}