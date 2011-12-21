using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsToolkit.Controls
{
	public enum ColorType {
		SystemColor,
		NamedColor,
		Custom
	}

	public enum ColorSortOrder {
		Brightness,
		Saturation,
		Hue
	}

	internal enum ColorState {
		Normal,
		MouseOver,
		Clicked,
		Selected
	}

	/// <summary>
	/// Description r�sum�e de ColorPalette.
	/// </summary>
	public class ColorPalette : System.Windows.Forms.UserControl
	{
		private System.ComponentModel.IContainer components;
		public event EventHandler SelectedColorChanged;
		public new event EventHandler Click;
		public new event EventHandler DoubleClick;
		private ColorsCollection colors;
		private System.Windows.Forms.ToolTip toolTip1;
		private bool isPainting = false;
		private ColorItem[] ci;
		private bool hover = false;

		public ColorPalette()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			this.SetStyle(
				ControlStyles.DoubleBuffer |
				ControlStyles.SupportsTransparentBackColor |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.UserPaint, true);

			colors = new ColorsCollection();
			colors.CollectionChanged += new EventHandler(colors_CollectionChanged);
		}

		
		#region Propri�t�s
		[Category("Appearance"), Description("D�fini la liste des couleurs qui seront affich�es dans le contr�le"),
		 DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ColorsCollection Colors {
			get {return colors;}
			set {
				if (colorType != ColorType.Custom) {
					//throw new Exception("Impossible de modifiers la liste des couleurs pour " + colorType.ToString());
				} else {
					colors = value;
				}
			}
		}

		private ColorType colorType = ColorType.NamedColor;
		[Category("Appearance"), DefaultValue(typeof(ColorType), "NamedColor"),
		 Description("D�fini le type de couleur qui sera affich� dans le contr�le.")]
		public ColorType ColorType {
			get {return colorType;}
			set {
				if (colorType != value) {
					colors.Clear();
					colorType = value;
					if (colorType == ColorType.SystemColor || 
						colorType == ColorType.NamedColor) {
						LoadColor();
					} /* else {
						BuildPalette();
					}*/
				}
				this.Invalidate();
			}
		}

		private ColorSortOrder colorSortOrder = ColorSortOrder.Brightness;
		[Category("Behavior"), Description("D�fini l'ordre d'affichage des couleurs")]
		public ColorSortOrder ColorSortOrder {
			get {return colorSortOrder;}
			set {colorSortOrder = value;
				BuildPalette();
				this.Invalidate();
			}
		}

		private bool autoSize = false;
		[DefaultValue(true), Description("Obtient ou d�fini une valeur qui d�termine si le contr�le se redimensionne automatiquement"),
		 Category("Behavior")]
		public new bool AutoSize {
			get {return autoSize;}
			set {
				if (autoSize != value) {
					autoSize = value;
					this.SetStyle(ControlStyles.FixedHeight | ControlStyles.FixedWidth, value);
					BuildPalette();
					this.Invalidate();
				}
			}
		}

		private int border = 5;
		[Description("Obtient ou d�fini l'espace qui s�pare les couleurs de la bordure du contr�le"),
		 Category("Appearance"), DefaultValue(5)]
		public int BorderPadding {
			get {return border;}
			set {
				if (border != value) {
					border = value;
					this.BuildPalette();
					this.Invalidate();
				}
			}
		}

		private int space = 5;
		[Description("Obtient ou d�fini l'espace qui s�pare chaque couleur"),
		 Category("Appearance"), DefaultValue(5)]
		public int ButtonSpacing {
			get {return space;}
			set {
				if (space != value) {
					space = value;
					this.BuildPalette();
					this.Invalidate();
				}
			}
		}

		private Size cSize = new Size(16,16);
		[Category("Appearance"), Description("Obtient ou d�fini la taille d'une couleur sur la palette"),
		 DefaultValue(typeof(Size), "16;16")]
		public Size ColorButtonSize {
			get {return cSize;}
			set {cSize = value;
				BuildPalette();
				this.Invalidate();
			}
		}

		private Color selectedColor = Color.White;
		[Description("Obtient ou d�fini la couleur s�lectionn�e"), Category("Data"),
		 DefaultValue(typeof(Color),"White")]
		public Color SelectedColor {
			get {return selectedColor;}
			set {
				if (selectedColor != value) {
					selectedColor = value;
					if (SelectedColorChanged != null) {
						SelectedColorChanged(this, EventArgs.Empty);
					}
					this.Invalidate(true);
				}
			}
		}

		#endregion
		
		#region Surcharges
		#region gestion des �v�nements souris
		protected override void OnPaint(PaintEventArgs e) 
		{
			if (!isPainting) {
				isPainting = true;
				//base.OnPaint (e);
				try 
				{
					DrawColorItems(e.Graphics);
				} 
				catch{}
				isPainting = false;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove (e);
			if (hover) {
				Point pt = this.PointToClient(MousePosition);
				ColorItem ci = GetColorItemFormPoint(pt);
				if (ci != null) 
				{
					this.toolTip1.SetToolTip(this, ci.ToolTipText);
				} 
				else 
				{
					this.toolTip1.SetToolTip(this, string.Empty);
				}
			}
			this.Invalidate();
		}

		protected override void OnClick(EventArgs e)
		{
			Point pt = this.PointToClient(MousePosition);
			ColorItem ci = GetColorItemFormPoint(pt);
			if (ci != null) 
			{
				this.selectedColor = ci.Color;
				if (this.Click != null) {
					this.Click(this, EventArgs.Empty);
				}
			}
			base.OnClick (e);
		}

		protected override void OnDoubleClick(EventArgs e)
		{
			Point pt = this.PointToClient(MousePosition);
			ColorItem ci = GetColorItemFormPoint(pt);
			if (ci != null) 
			{
				if (this.DoubleClick != null) 
				{
					this.DoubleClick(this, EventArgs.Empty);
				}
			}
			base.OnDoubleClick (e);
		}

		protected override void OnMouseHover(EventArgs e)
		{
			hover = true;
			base.OnMouseHover (e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			hover = false;
			base.OnMouseLeave (e);
		}

		#endregion
		protected override void OnResize(EventArgs e) 
		{
			// on r�organise la palette par rapport � la taille du controle
			BuildPalette();
			base.OnResize (e);
		}

		/// <summary> 
		/// Nettoyage des ressources utilis�es.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Code g�n�r� par le Concepteur de composants
		/// <summary> 
		/// M�thode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette m�thode avec l'�diteur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			// 
			// ColorPalette
			// 
			this.Name = "ColorPalette";

		}
		#endregion

		#region M�thodes priv�es
		/// <summary>
		/// Dessine tous les �l�ments de couleur dans 
		/// le Graphics pass� en param�tre
		/// </summary>
		/// <param name="g"></param>
		private void DrawColorItems(Graphics g) {
			Point mousePt = this.PointToClient(MousePosition);
			ColorState state = ColorState.Normal;

			for (int i=0; i<this.colors.Count; i++) 
			{
				if (ci[i] == null)
					break;

				state = ColorState.Normal;
				// Affiche un rectangle avec la couleur
				g.FillRectangle(new SolidBrush(this.colors[i]), this.ci[i].Bounds);		

				// Si c'est la couleur s�lectionn�e, on met l'�tat � Selected
				// sinon, on v�rifie la position de la souris pour dessiner la bordure
				if (ci[i].Color != this.selectedColor) 
				{
					if (GetColorItemFormPoint(mousePt) == ci[i]) 
					{
						state = (MouseButtons == MouseButtons.Left ? ColorState.Clicked : ColorState.MouseOver);	
					}
				} 
				else {
					state = ColorState.Selected;
				}

				// Dessine la bordure du rectangle en fonction
				// de l'�tat pr�alablement renseign�
				switch(state) 
				{
					default:
					// Normal : bordure fine et couleur syst�me ControlDark
					case ColorState.Normal:
						g.DrawRectangle(SystemPens.ControlDark, this.ci[i].Bounds);
						break;
					// MouseOver : bordure fine et "effet 3D" : le haut clair, le bas fonc�
					case ColorState.MouseOver:
						g.DrawLine(SystemPens.ControlLightLight, this.ci[i].Left , this.ci[i].Top, this.ci[i].Left + this.ci[i].Width, this.ci[i].Top);
						g.DrawLine(SystemPens.ControlLightLight, this.ci[i].Left , this.ci[i].Top, this.ci[i].Left, this.ci[i].Top + this.ci[i].Height);
						g.DrawLine(SystemPens.ControlDarkDark, this.ci[i].Left + this.ci[i].Width , this.ci[i].Top + this.ci[i].Height, this.ci[i].Left + this.ci[i].Width, this.ci[i].Top);
						g.DrawLine(SystemPens.ControlDarkDark, this.ci[i].Left + this.ci[i].Width , this.ci[i].Top + this.ci[i].Height, this.ci[i].Left, this.ci[i].Top + this.ci[i].Height);
						break;
					// Clicked : bordure fine et "effet 3D inverse" : le haut fonc� et le bas clair
					case ColorState.Clicked:
						g.DrawLine(SystemPens.ControlDarkDark, this.ci[i].Left , this.ci[i].Top, this.ci[i].Left + this.ci[i].Width, this.ci[i].Top);
						g.DrawLine(SystemPens.ControlDarkDark, this.ci[i].Left , this.ci[i].Top, this.ci[i].Left, this.ci[i].Top + this.ci[i].Height);
						g.DrawLine(SystemPens.ControlLightLight, this.ci[i].Left + this.ci[i].Width , this.ci[i].Top + this.ci[i].Height, this.ci[i].Left + this.ci[i].Width, this.ci[i].Top);
						g.DrawLine(SystemPens.ControlLightLight, this.ci[i].Left + this.ci[i].Width , this.ci[i].Top + this.ci[i].Height, this.ci[i].Left, this.ci[i].Top + this.ci[i].Height);
						break;
					// Selected : bordure �paisse et couleur syst�me Highlight
					case ColorState.Selected:
						Size size = new Size(ci[i].Width-2, ci[i].Height-2);
						g.DrawRectangle(new Pen(SystemPens.Highlight.Color, 2), this.ci[i].Left+1 , this.ci[i].Top+1, size.Width, size.Height);
						break;
				}
			}
		}

		/// <summary>
		/// Recherche l'�l�ment qui se trouve en dessous de la souris
		/// </summary>
		/// <param name="pt">Position de la souris dans le contr�le</param>
		/// <returns>Item si trouv�, sinon null</returns>
		private ColorItem GetColorItemFormPoint(Point pt) {
			for (int i=0; i<this.colors.Count; i++) {
				if (ci[i] != null && ci[i].Bounds.Contains(pt)) 
					return ci[i];
			}
			return null;
		}

		/// <summary>
		/// Charge les couleurs correspondant au type
		/// </summary>
		private void LoadColor() {
			// D�connecte l'�v�nement sur la collection de couleurs
			// pour �viter de rafraichir le control inutilement
			colors.CollectionChanged -= new System.EventHandler(colors_CollectionChanged);
			colors.Clear();
			foreach(string c in Enum.GetNames(typeof(System.Drawing.KnownColor))) {
				if ((colorType == ColorType.SystemColor && Color.FromName(c).IsSystemColor) || 
					(colorType == ColorType.NamedColor && !Color.FromName(c).IsSystemColor)) {
					colors.Add(Color.FromName(c));
				}
			}
			// R�organise la palette avec les nouvelles couleurs
			BuildPalette();
			// Reconnecte l'�v�nement sur la modification de la collection
			// de couleur
			colors.CollectionChanged += new System.EventHandler(colors_CollectionChanged);
		}

		/// <summary>
		/// Oragnise les couleurs dans le controle
		/// </summary>
		private void BuildPalette() {
			int x = border;
			int y = border;
			Size size = cSize;
			if (this.colors.Count == 0)
				return;

			// Trie les couleur dans l'ordre pr�d�fini
			this.colors.Sort(new ColorComparer(this.colorSortOrder));
			if (this.autoSize) {
				// calcul de la taille id�ale
				// = Racine carr�e du nombre d'�l�ments

				int larg = (int)Math.Sqrt(this.colors.Count);
				int haut = (int)Math.Ceiling((double)this.colors.Count/(double)larg);

				// Si l'�cart entre les 2 est sup�rieur � 1 
				// alors on aura un d�s�quilibre
				// donc on ajoute � la largeur la moiti� du nombre
				// d'�l�ments
				if (haut - larg > 1) {
					larg += (haut - larg)/2;
					haut -= (haut - larg)/2;
				}
				
				// Affect� les valeurs trouv�es � la largeur et la hauteur
				int sizeLarg = 2 * border + (larg - 1) * space + larg * cSize.Width;
				int sizeHaut = 2 * border + (haut - 1) * space + haut * cSize.Height;

				// on change la taille du controle
				this.Size = new Size(sizeLarg, sizeHaut);
			}

			this.SuspendLayout();
			// Supprime les sous-controles
			ci = new ColorItem[this.colors.Count];

			for (int i=0; i<this.colors.Count; i++) {
				// Ajoute une nouvelle couleur
				ci[i] = new ColorItem();
				ci[i].Location = new Point(x,y);
				ci[i].Size = size;
				ci[i].Color = this.colors[i];
				ci[i].ToolTipText = this.colors[i].Name;

				// Calcul les coordonn�es de la prochaine couleur
				// x:
				x += size.Width + space;
				if (x + size.Width + border > this.Width) {
					x = border;
					y += size.Height + space;
				}

				// y:
				if (y + size.Height + border > this.Height) {
					break;
				}
			}
			this.ResumeLayout();
		}
		#endregion

		#region Ev�nements
		private void colors_CollectionChanged(object sender, EventArgs e) {
			BuildPalette();
		}
		#endregion

		#region Sous-classes
		/// <summary>
		/// Classe permettant de repr�senter un �l�ment de couleur
		/// </summary>
		internal class ColorItem {
			private Color color;
			public Color Color {
				get {return color;}
				set {color = value;}
			}
			
			private string toolTipText = string.Empty;
			public string ToolTipText 
			{
				get {return this.toolTipText;}
				set {this.toolTipText = value;}
			}

			private Size size;
			public Size Size {
				get {return size;}
				set {size = value;}
			}

			private Point location;
			public Point Location {
				get {return location;}
				set {location = value;}
			}

			public int Left {
				get {return location.X;}
				set {location.X = value;}
			}

			public int Top {
				get {return location.Y;}
				set {location.Y = value;}
			}

			public int Height {
				get {return size.Height;}
				set {size.Height = value;}
			}

			public int Width {
				get {return size.Width;}
				set {size.Width = value;}
			}

			public Rectangle Bounds {
				get {return new Rectangle(location, size);}
			}
		}

		public class ColorsCollection : System.Collections.CollectionBase {
			public event EventHandler CollectionChanged;
			public ColorsCollection() {}

			public int Add(Color value) {
				int ret = base.InnerList.Add(value);
				if (CollectionChanged != null)
					CollectionChanged(this, new EventArgs());
				return ret;
			}

			public void AddRange(Color[] values) {
				base.InnerList.AddRange(values);
				if (CollectionChanged != null)
					CollectionChanged(this, new EventArgs());
			}

			public void Remove(Color value) {
				base.InnerList.Remove(value);
				if (CollectionChanged != null)
					CollectionChanged(this, new EventArgs());
			}

			public void RemoveRange(int index, int count) {
				base.InnerList.RemoveRange(index, count);
				if (CollectionChanged != null)
					CollectionChanged(this, new EventArgs());
			}

			public new void Clear() {
				base.InnerList.Clear();
				if (CollectionChanged != null)
					CollectionChanged(this, new EventArgs());
			}


			public void Sort() {
				base.InnerList.Sort();
			}

			public void Sort(IComparer comparer) {
				base.InnerList.Sort(comparer);
			}

			public void Sort(int index, int count, IComparer comparer) {
				base.InnerList.Sort(index, count, comparer);
			}

			public Color this[int index] {
				get {return (Color)base.InnerList[index];}
				set {base.InnerList[index] = value;}
			}

		}

		/// <summary>
		/// Va permettre de comparer les couleurs
		/// </summary>
		private class ColorComparer : IComparer {
			#region Membres de IComparer
			private ColorSortOrder order = ColorSortOrder.Brightness;
			public ColorComparer(ColorSortOrder order) {
				this.order = order;
			}

			public int Compare(object x, object y) {
				Color cx = (Color)x;
				Color cy = (Color)y;

				// Compare les couleurs suivant l'un des trois crit�res
				switch(this.order) {
					default:
					case ColorSortOrder.Brightness:
						return cy.GetBrightness().CompareTo(cx.GetBrightness());
					case ColorSortOrder.Hue:
						return cy.GetHue().CompareTo(cx.GetHue());
					case ColorSortOrder.Saturation :
						return cy.GetSaturation().CompareTo(cx.GetSaturation());
				}
			}

			#endregion
		}
		#endregion
	}
}
