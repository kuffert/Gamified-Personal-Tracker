using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Author: Craig Hammond
// Date: 11/23/2015
public class Experience {
    private Dictionary<string, int> expTotals;

    private Experience() {
        this.expTotals = new Dictionary<string, int>();
    }

    public void add(string dimension, int exp)
    {
        int val = 0;
        this.expTotals.TryGetValue(dimension, out val);
        this.expTotals.Add(dimension, exp + val);
    }

    public void add(Experience exp)
    {
        foreach (KeyValuePair<string, int> pair in exp.expTotals)
        {
            this.add(pair.Key, pair.Value);
        }
    }

    public Dictionary<string, int> totals
    {
        get
        {
            return this.expTotals;
        }
    }

    public static Experience DefaultExperience()
    {
        Experience e = new Experience();
        foreach (int value in Enum.GetValues(typeof(Dimension)))
        {
            e.add(((Dimension)value).GetDescription(), 0);
        }

        return e;
    }
}
