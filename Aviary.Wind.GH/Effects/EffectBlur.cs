using System;
using System.Collections.Generic;
using System.Drawing;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wg = Aviary.Wind.Graphics;

namespace Aviary.Wind.GH
{
    public class EffectBlur : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the BlurEffect class.
        /// </summary>
        public EffectBlur()
          : base("Blur Effect", "Blur", "Add a blur effect", "Aviary 1", "Graphics")
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
            pManager.AddNumberParameter("Radius", "R", "The blur radius", GH_ParamAccess.item, 1.0);
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
            Wg.Graphic graphic = new Wg.Graphic();
            if (!DA.GetData(0, ref graphic)) graphic = new Wg.Graphic(graphic);

            double radius = 1.0;
            DA.GetData(1, ref radius);

            graphic.Effects.Blur = new Wg.BlurEffect(radius);

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
                return Properties.Resources.EffectBlur;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e04b9525-3208-4921-a283-ff243ece7536"); }
        }
    }
}