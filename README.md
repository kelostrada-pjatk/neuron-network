Neuron Network - Simple project
==============

Polish documents explaining how this network works can be found in Docs\ directory.

The solution consists of three projects:
- NetIn
- NeuronNetwork
- NeuronNetwork.MatrixInput

Whole solution tries to teach the network to be able to recognize if given matrices are "correct". 
Every matrix is binary and 3x3. In our basic problem we have 5 such matrices. 
Each of these matrices corresponds to a single digit. The number of "ones" describes which number is the matrix.

So for example this matrix corresponds to number `6`:

<table>
<tr><td>1</td><td>1</td><td>0</td></tr>
<tr><td>0</td><td>1</td><td>0</td></tr>
<tr><td>1</td><td>1</td><td>1</td></tr>
</table>

In this project I'm trying to find a number `11027` but you can set it to anything else.


NetIn
-----

This project is a console application used for running the network. 
It references the other two projects and the main aspect of this 
project is to create the neuron network and test when it will learn the given problem.

NeuronNetwork
-------------

This project is a library project with most of the Neuron Network standard logic (Perceptrons and connections between them)

NeuronNetwork.MatrixInput
-------------------------

This project is a library which references to `NeuronNetwork`. In it you can find a simple class `Matrix` 
which is used for the single digit representations (and it gives you a possibility to create some random matrices).
Also there is a class Called `MatrixInputNetwork` which gives all network nodes a proper order of making calculations 
and backwards propagations.
