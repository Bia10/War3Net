﻿// ------------------------------------------------------------------------------
// <copyright file="CreateAllDestructables.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using War3Net.Build.Extensions;
using War3Net.Build.Widget;
using War3Net.CodeAnalysis.Jass.Syntax;

using static War3Api.Common;

using SyntaxFactory = War3Net.CodeAnalysis.Jass.JassSyntaxFactory;

namespace War3Net.Build
{
    public partial class MapScriptBuilder
    {
        protected virtual JassFunctionDeclarationSyntax CreateAllDestructables(Map map)
        {
            if (map is null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            var mapDoodads = map.Doodads;
            if (mapDoodads is null)
            {
                throw new ArgumentException($"Function '{nameof(CreateAllDestructables)}' cannot be generated without {nameof(MapDoodads)}.", nameof(map));
            }

            var statements = new List<IStatementSyntax>();
            statements.Add(SyntaxFactory.LocalVariableDeclarationStatement(SyntaxFactory.ParseTypeName(nameof(destructable)), VariableName.Destructable));
            statements.Add(SyntaxFactory.LocalVariableDeclarationStatement(SyntaxFactory.ParseTypeName(nameof(trigger)), VariableName.Trigger));

            if (UseLifeVariable)
            {
                statements.Add(SyntaxFactory.LocalVariableDeclarationStatement(JassTypeSyntax.Real, VariableName.Life));
            }

            var createFunctions = new[]
            {
                nameof(CreateDestructable),
                nameof(CreateDeadDestructable),
                nameof(CreateDestructableZ),
                nameof(CreateDeadDestructableZ),
                nameof(BlzCreateDestructableWithSkin),
                nameof(BlzCreateDeadDestructableWithSkin),
                nameof(BlzCreateDestructableZWithSkin),
                nameof(BlzCreateDeadDestructableZWithSkin),
            };

            foreach (var (destructable, id) in mapDoodads.Doodads.IncludeId().Where(pair => CreateAllDestructablesConditionSingleDoodad(map, pair.Obj)))
            {
                var isDead = destructable.Life == 0;
                var hasZ = destructable.State.HasFlag(DoodadState.WithZ);
                var hasSkin = destructable.SkinId != 0 && destructable.SkinId != destructable.TypeId;
                var createFunctionIndex = isDead ? 1 : 0;

                var arguments = new List<IExpressionSyntax>();
                arguments.Add(SyntaxFactory.FourCCLiteralExpression(destructable.TypeId));
                arguments.Add(SyntaxFactory.LiteralExpression(destructable.Position.X));
                arguments.Add(SyntaxFactory.LiteralExpression(destructable.Position.Y));
                if (hasZ)
                {
                    arguments.Add(SyntaxFactory.LiteralExpression(destructable.Position.Z));
                    createFunctionIndex += 2;
                }

                arguments.Add(SyntaxFactory.LiteralExpression(destructable.Rotation * (180f / MathF.PI), precision: 3));
                arguments.Add(SyntaxFactory.LiteralExpression(destructable.Scale.X, precision: 3));
                arguments.Add(SyntaxFactory.LiteralExpression(destructable.Variation));
                if (hasSkin)
                {
                    arguments.Add(SyntaxFactory.FourCCLiteralExpression(destructable.SkinId));
                    createFunctionIndex += 4;
                }

                statements.Add(SyntaxFactory.SetStatement(
                    VariableName.Destructable,
                    SyntaxFactory.InvocationExpression(createFunctions[createFunctionIndex], arguments.ToArray())));

                if (!isDead && destructable.Life != 100)
                {
                    if (UseLifeVariable)
                    {
                        statements.Add(SyntaxFactory.SetStatement(
                            VariableName.Life,
                            SyntaxFactory.InvocationExpression(
                                nameof(GetDestructableLife),
                                SyntaxFactory.VariableReferenceExpression(VariableName.Destructable))));

                        statements.Add(SyntaxFactory.CallStatement(
                            nameof(SetDestructableLife),
                            SyntaxFactory.VariableReferenceExpression(VariableName.Destructable),
                            SyntaxFactory.BinaryMultiplicationExpression(
                                SyntaxFactory.LiteralExpression(destructable.Life * 0.01f, precision: 2),
                                SyntaxFactory.VariableReferenceExpression(VariableName.Life))));
                    }
                    else
                    {
                        statements.Add(SyntaxFactory.CallStatement(
                            nameof(SetDestructableLife),
                            SyntaxFactory.VariableReferenceExpression(VariableName.Destructable),
                            SyntaxFactory.BinaryMultiplicationExpression(
                                SyntaxFactory.LiteralExpression(destructable.Life * 0.01f, precision: 2),
                                SyntaxFactory.InvocationExpression(nameof(GetDestructableLife), SyntaxFactory.VariableReferenceExpression(VariableName.Destructable)))));
                    }
                }

                statements.Add(SyntaxFactory.SetStatement(
                    VariableName.Trigger,
                    SyntaxFactory.InvocationExpression(nameof(CreateTrigger))));

                statements.Add(SyntaxFactory.CallStatement(
                    nameof(TriggerRegisterDeathEvent),
                    SyntaxFactory.VariableReferenceExpression(VariableName.Trigger),
                    SyntaxFactory.VariableReferenceExpression(VariableName.Destructable)));

                statements.Add(SyntaxFactory.CallStatement(
                    nameof(TriggerAddAction),
                    SyntaxFactory.VariableReferenceExpression(VariableName.Trigger),
                    SyntaxFactory.FunctionReferenceExpression(nameof(War3Api.Blizzard.SaveDyingWidget))));

                statements.Add(SyntaxFactory.CallStatement(
                    nameof(TriggerAddAction),
                    SyntaxFactory.VariableReferenceExpression(VariableName.Trigger),
                    SyntaxFactory.FunctionReferenceExpression(destructable.GetDropItemsFunctionName(id))));
            }

            return SyntaxFactory.FunctionDeclaration(SyntaxFactory.FunctionDeclarator(nameof(CreateAllDestructables)), statements);
        }

        protected virtual bool CreateAllDestructablesCondition(Map map)
        {
            if (map is null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            return map.Doodads is not null
                && map.Doodads.Doodads.Any(doodad => CreateAllDestructablesConditionSingleDoodad(map, doodad));
        }

        protected virtual bool CreateAllDestructablesConditionSingleDoodad(Map map, DoodadData doodadData)
        {
            if (map is null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            if (doodadData is null)
            {
                throw new ArgumentNullException(nameof(doodadData));
            }

            return doodadData.ItemTableSets.Any() || doodadData.MapItemTableId != -1;
        }
    }
}