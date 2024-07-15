using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationTest : Mutation
{
    protected override void Awake(){
        base.Awake();
        mutationId = "TEST";
    }
}
