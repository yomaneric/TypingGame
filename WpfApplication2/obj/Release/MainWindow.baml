   M S B A M L   `   `   `  ���� J  FWpfApplication2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication2WpfApplication2       WpfApplication2.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:WpfApplication2       � Title$  Typing Game��$	��350��$	��525������  �� ImageSource$ p05xh5dx.jpg�� �� Opacity$	 0.8Q� j� Stretch$ UniformToFill=�.��(�  (� Stretch$
 Fill=�.��� .+��� -    ��Name$ label��$
��Left=�$��37��$��0,1,0,0q�$	��Top=�$	��520��
���   ��� -    ��Name$ TimeRemaining��$
��Left=�$��21��$��
341,15,0,0q�$
��Wrap=�$��Time Remaining: ��$	��Top=�$	��144��
��������� -    ��Name$ TimeCounter��$
��Left=�$��21��$��
435,15,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$��75��$��0q�
�������
���   ��� -   $ HealthBlock��$
��Left=�$��21��$��	41,15,0,0q�$
��Wrap=�$��Health:��$	��Top=�$��59��
��������� -   $ 
ScoreBlock��$
��Left=�$��21��$��
196,15,0,0q�$
��Wrap=�$��Score:��$	��Top=�$��59��
��������� -   $ TypeBox��$
��Left=�$��43��$��	0,280,0,0q�$
 Wrap=�$	��Top=�$	��520��$	��.01q�
���   �	 �� TextAlignment$	 Center=�$��24(�
�������.��
 �� SelectionBrush
 ��  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ Black�� � Offset$ 1Q�� $ 	#FF342F79��.������ $ 0.5,1&� � MappingMode$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ 	#FF454370��$ 0.016Q�� $ 	#FF0D0B30��$ 0.096Q�.���� -   $ ScoreCounter��$
��Left=�$��21��$��
234,15,0,0q�$
 Wrap=�$	��Top=�$��75��$��0q�$��0��
���   �
�������� -    �Name$ TextBlockArea��$
��Left=�$	��237��$��0,43,0,0q�$	��Top=�$	��520��� -	   $ LifeGrid��$
��Left=�$��21��$��	83,14,0,0q�$	��Top=�$��73��.+��� -
    ��Name$ Life1��$��
Heart1.png��$
��Left=�$��21��$	��Top=�$��21�� J� RenderTransformOrigin$ 0.339,0.393&��� -   $ Life2��$��
Heart1.png��$
��Left=�$��21��$��26,0,0,0q�$	��Top=�$��21���� -   $ Life3��$��
Heart1.png��$
��Left=�$��21��$��52,0,0,0q�$	��Top=�$��21��$ 2.744,0.393&��� -   $
 Dead��$
��Left=�$��5,2,0,-1q�$
��Wrap=�$��68��$ -0.594,0.48&��� -    ��Name$ StartButton��$��Start��$
��Left=�$��49��$��196,149,0,0q�$	��Top=�$	��110��$��22(�$
��Bold"�$��Italic$�$��0q����	#FFF9F9F9��Z� �� -   $��Canvas2D��$
��Left=�$	��237��$��0,43,0,0q�$	��Top=�$	��520���� ���	#FF005DFF$
��Left=�$��1��$��	0,280,0,0q����	#FF005DFF$��10��$	��Top=�$	��520���� -   $ 	NextLevel��$��
Next Level��$
��Left=�$��49��$��
196,83,0,0q�$	��Top=�$	��110��$��22(�$
��Bold"�$��Italic$�$��0q����	#FFF9F9F9��Z� 