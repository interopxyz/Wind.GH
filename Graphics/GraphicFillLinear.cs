using System;
using System.Drawing;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wg = Aviary.Wind.Graphics;

namespace Aviary.Wind.GH
{
    public class GraphicFillLinear : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GraphicFillLinear class.
        /// </summary>
        public GraphicFillLinear()
          : base("Linear Gradient Fill", "Linear", "Add a linear gradient fill", "Aviary 1", "Graphics")
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
            pManager.AddNumberParameter("Angle", "A", "The rotation angle in degrees", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;
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

            double angle = 0;
            DA.GetData(3, ref angle);

            List<Wg.Color> windColors = new List<Wg.Color>();
            foreach(Color color in colors)
            {
                windColors.Add(color.ToWindColor());
            }

            Wg.GradientLinear gradient = new Wg.GradientLinear(windColors,stops);
            gradient.Angle = angle;
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
                return Properties.Resources.FillGradientLinear;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("5f94c911-0fd5-486e-817f-ed99ef798526"); }
        }
    }
}