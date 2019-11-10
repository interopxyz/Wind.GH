using System;
using System.Drawing;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wg = Aviary.Wind.Graphics;

namespace Aviary.Wind.GH
{
    public class GraphicFillSolid : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GraphicFillSolid class.
        /// </summary>
        public GraphicFillSolid()
          : base("Solid Fill", "Solid", "Add a solid color fill", "Aviary 1", "Graphics")
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
            pManager.AddColourParameter("Fill Color", "C", "The fill color for the graphic", GH_ParamAccess.item, Color.Black);
            pManager[1].Optional = true;
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
            Color color = Color.Black;
            if (!DA.GetData(0, ref graphic)) graphic = new Wg.Graphic(graphic);
            DA.GetData(1, ref color);

            graphic.Fill.Background = color.ToWindColor();

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
                return Properties.Resources.FillSolid;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("7213979a-8ec1-4d52-bf2f-b0f22551ea4d"); }
        }
    }
}