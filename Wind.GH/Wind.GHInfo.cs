﻿using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Wind.GH
{
    public class WindGHInfo : GH_AssemblyInfo
  {
    public override string Name
    {
        get
        {
            return "WindGH";
        }
    }
    public override Bitmap Icon
    {
        get
        {
            //Return a 24x24 pixel bitmap to represent this GHA library.
            return null;
        }
    }
    public override string Description
    {
        get
        {
            //Return a short string describing the purpose of this GHA library.
            return "";
        }
    }
    public override Guid Id
    {
        get
        {
            return new Guid("d51a3043-1844-4181-a413-f48120bd462f");
        }
    }

    public override string AuthorName
    {
        get
        {
            //Return a string identifying you or your company.
            return "";
        }
    }
    public override string AuthorContact
    {
        get
        {
            //Return a string representing your preferred contact details.
            return "";
        }
    }
}
}
