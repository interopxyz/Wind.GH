using System;
using System.Collections.Generic;
using System.Drawing;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wg = Wind.Graphics;

namespace Wind.GH.Effects
{
    public class EffectShadow : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ShadowEffect class.
        /// </summary>
        public EffectShadow()
          : base("Shadow Effect", "Shadow", "Description", "Display", "Graphics")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Graphic", "G", "An optional Graphic object", GH_ParamAccess.item);
            pManager[0].Optional = true;
            pManager.AddColourParameter("Color", "C", "The fill color for the graphic", GH_ParamAccess.item, Color.Black);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Radius", "R", "The blur radius", GH_ParamAccess.item, 1.0);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Angle", "A", "The angle of the shadow offset in degrees", GH_ParamAccess.item, 0.0);
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Distance", "D", "The distance of the shadow offset", GH_ParamAccess.item, 0.0);
            pManager[4].Optional = true;
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
            Wg.Graphic graphic = new Wg.Graphic();
            if (!DA.GetData(0, ref graphic)) graphic = new Wg.Graphic(graphic);

            Color color = Color.Black;
            DA.GetData(1, ref color);

            double radius = 1.0;
            DA.GetData(2, ref radius);
            double angle = 0.0;
            DA.GetData(3, ref angle);
            double distance = 0.0;
            DA.GetData(4, ref distance);

            graphic.Effects.Shadow = new Wg.ShadowEffect(color.ToWindColor(), radius, angle, distance);

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
                return Properties.Resources.EffectDropShadow24;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("98bca6ac-abe3-463f-8b9d-7c712ec4d05e"); }
        }
    }
}