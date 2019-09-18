using System;
using System.Drawing;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wg = Aviary.Wind.Graphics;

namespace Aviary.Wind.GH
{
    public class GraphicFillRadial : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GraphicFillRadial class.
        /// </summary>
        public GraphicFillRadial()
          : base("Radial Gradient Fill", "Radial", "Add a radial gradient fill", "Aviary 1", "Graphics")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graphic", "G", "An optional Graphic object", GH_ParamAccess.item);
            pManager[0].Optional = true;
            pManager.AddColourParameter("Gradient Colors", "C", "The gradient colors for the graphic", GH_ParamAccess.list, new List<Color> { Color.Black, Color.White });
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Parameters", "P", "The gradient's colors parameters", GH_ParamAccess.list, new List<double> { 0, 1 });
            pManager[2].Optional = true;
            pManager.AddPointParameter("Center", "CP", "The center point of the gradient", GH_ParamAccess.item, new Point3d(0.5,0.5,0));
            pManager[3].Optional = true;
            pManager.AddPointParameter("Focus", "CF", "The focal point of the gradient", GH_ParamAccess.item, new Point3d(0.5, 0.5, 0));
            pManager[4].Optional = true;
            pManager.AddNumberParameter("Radius", "R", "The radius of the gradient", GH_ParamAccess.item, 1.0);
            pManager[5].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Graphic", "G", "The resulting Graphic object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Wg.Graphic graphic = Wg.Graphics.FillBlack;
            if (!DA.GetData(0, ref graphic)) graphic = new Wg.Graphic(graphic);

            List<Color> colors = new List<Color>();
            DA.GetDataList(1, colors);

            List<double> stops = new List<double>();
            DA.GetDataList(2, stops);

            Point3d center = new Point3d(0.5,0.5,0);
            DA.GetData(3, ref center);

            Point3d focus = new Point3d(0.5, 0.5, 0);
            DA.GetData(4, ref focus);

            double radius = 1.0;
            DA.GetData(5, ref radius);

            List<Wg.Color> windColors = new List<Wg.Color>();
            foreach (Color color in colors)
            {
                windColors.Add(color.ToWindColor());
            }

            Wg.GradientRadial gradient = new Wg.GradientRadial(windColors, stops);
            gradient.Center = new Wind.Geometry.Point(center.X, center.Y, 0);
            gradient.Focus = new Wind.Geometry.Point(focus.X, focus.Y, 0);
            gradient.RadiusX = radius;
            gradient.RadiusY = radius;

            graphic.Fill = gradient;

            DA.SetData(0, graphic);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Properties.Resources.FillGradientRadial;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("1abb7fe4-2006-4917-bfeb-b50e2b309b07"); }
        }
    }
}