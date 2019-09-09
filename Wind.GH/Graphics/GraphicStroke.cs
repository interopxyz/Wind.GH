using System;
using System.Drawing;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wg = Wind.Graphics;

namespace Wind.GH.Graphics
{
    public class GraphicStroke : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GraphicStroke class.
        /// </summary>
        public GraphicStroke()
          : base("Stroke", "Stroke", "Set Stroke Properties", "Display", "Graphics")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graphic", "G", "An optional Graphic object", GH_ParamAccess.item);
            pManager[0].Optional = true;
            pManager.AddColourParameter("Stroke Color", "C", "The fill color for the graphic", GH_ParamAccess.item, Color.Black);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Weight", "W", "The Stroke weight", GH_ParamAccess.item, 1.0);
            pManager[2].Optional = true;
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
            Wg.Graphic graphic = Wg.Graphics.StrokeBlack;
            if (!DA.GetData(0, ref graphic)) graphic = new Wg.Graphic(graphic);

            Color color = Color.Black;
            DA.GetData(1, ref color);

            double weight = 1.0;
            DA.GetData(2, ref weight);

            graphic.Stroke.Color= color.ToWindColor();
            graphic.Stroke.Weight = weight;

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
                return Properties.Resources.Stroke;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("6080b922-a2c1-4fa6-95ff-58f1618a9bc9"); }
        }
    }
}